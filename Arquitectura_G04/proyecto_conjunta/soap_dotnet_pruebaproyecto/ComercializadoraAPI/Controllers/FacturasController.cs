using Microsoft.AspNetCore.Mvc;
using ComercializadoraAPI.Data;
using ComercializadoraAPI.DTOs;
using ComercializadoraAPI.Models;
using ComercializadoraAPI.SoapClient;
using Microsoft.EntityFrameworkCore;

namespace ComercializadoraAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturasController : ControllerBase
    {
        private readonly ComercializadoraDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<FacturasController> _logger;

        public FacturasController(
            ComercializadoraDbContext context,
            IConfiguration configuration,
            ILogger<FacturasController> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<FacturaResponseDto>> CreateFactura(CreateFacturaDto createDto)
        {
            try
            {
                // 1. Validar que el cliente existe o crearlo
                var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Cedula == createDto.Cedula);
                if (cliente == null)
                {
                    return BadRequest("Cliente no existe. Por favor, registre el cliente primero.");
                }

                // 2. Validar forma de pago
                if (createDto.FormaPago.ToUpper() != "EFECTIVO" && createDto.FormaPago.ToUpper() != "CREDITO")
                {
                    return BadRequest("Forma de pago debe ser EFECTIVO o CREDITO");
                }

                // 3. Validar y calcular totales
                if (!createDto.Detalles.Any())
                {
                    return BadRequest("Debe incluir al menos un producto");
                }

                decimal subtotal = 0;
                var detallesFactura = new List<DetalleFactura>();

                foreach (var detalle in createDto.Detalles)
                {
                    var producto = await _context.Productos.FindAsync(detalle.IdProducto);
                    if (producto == null)
                    {
                        return BadRequest($"Producto con ID {detalle.IdProducto} no existe");
                    }

                    decimal totalLinea = producto.PrecioVenta * detalle.Cantidad;
                    subtotal += totalLinea;

                    detallesFactura.Add(new DetalleFactura
                    {
                        IdProducto = detalle.IdProducto,
                        Cantidad = detalle.Cantidad,
                        PrecioUnitario = producto.PrecioVenta,
                        TotalLinea = totalLinea
                    });
                }

                decimal descuento = 0;
                decimal total = subtotal;
                int? idCreditoBanco = null;
                string mensaje = "";
                bool aprobado = true;

                // 4. Procesar según forma de pago
                if (createDto.FormaPago.ToUpper() == "EFECTIVO")
                {
                    // Aplicar descuento del 33%
                    descuento = subtotal * 0.33m;
                    total = subtotal - descuento;
                    mensaje = "Factura procesada con descuento del 33%";
                }
                else // CREDITO
                {
                    // Validar plazo
                    if (createDto.PlazoMeses < 1 || createDto.PlazoMeses > 36)
                    {
                        return BadRequest("El plazo debe estar entre 1 y 36 meses");
                    }

                    // Llamar al servicio SOAP del banco
                    var bancoServiceUrl = _configuration["BancoService:Url"] ?? "http://localhost:5000/BancoService.asmx";
                    
                    using (var bancoClient = new BancoServiceClient(bancoServiceUrl))
                    {
                        var creditRequest = new EvaluateCreditRequest
                        {
                            Cedula = createDto.Cedula,
                            PrecioElectrodomestico = subtotal,
                            PlazoMeses = createDto.PlazoMeses
                        };

                        var creditResponse = await bancoClient.EvaluateCreditAsync(creditRequest);

                        if (!creditResponse.Aprobado)
                        {
                            // Crédito rechazado
                            return BadRequest(new
                            {
                                Mensaje = $"Crédito rechazado: {creditResponse.Mensaje}",
                                SujetoCredito = creditResponse.SujetoCredito,
                                MontoMaximo = creditResponse.MontoMaximo
                            });
                        }

                        // Crédito aprobado
                        idCreditoBanco = creditResponse.IdCredito;
                        mensaje = $"Crédito aprobado. ID Crédito: {creditResponse.IdCredito}";
                        _logger.LogInformation($"Crédito aprobado para {createDto.Cedula}. IdCredito: {creditResponse.IdCredito}");
                    }
                }

                // 5. Crear la factura
                var numeroFactura = GenerarNumeroFactura();
                var factura = new Factura
                {
                    NumeroFactura = numeroFactura,
                    IdCliente = cliente.IdCliente,
                    Fecha = DateTime.Now,
                    FormaPago = createDto.FormaPago.ToUpper(),
                    Subtotal = subtotal,
                    Descuento = descuento,
                    Total = total,
                    IdCreditoBanco = idCreditoBanco,
                    Detalles = detallesFactura
                };

                _context.Facturas.Add(factura);
                await _context.SaveChangesAsync();

                // 6. Retornar respuesta
                var response = new FacturaResponseDto
                {
                    IdFactura = factura.IdFactura,
                    NumeroFactura = factura.NumeroFactura,
                    Fecha = factura.Fecha,
                    FormaPago = factura.FormaPago,
                    Subtotal = factura.Subtotal,
                    Descuento = factura.Descuento,
                    Total = factura.Total,
                    IdCreditoBanco = factura.IdCreditoBanco,
                    Mensaje = mensaje,
                    Aprobado = aprobado
                };

                return CreatedAtAction(nameof(GetFactura), new { id = factura.IdFactura }, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear factura");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Factura>> GetFactura(int id)
        {
            try
            {
                var factura = await _context.Facturas
                    .Include(f => f.Cliente)
                    .Include(f => f.Detalles)
                    .ThenInclude(d => d.Producto)
                    .FirstOrDefaultAsync(f => f.IdFactura == id);

                if (factura == null)
                {
                    return NotFound("Factura no encontrada");
                }

                return Ok(factura);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener factura");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Factura>>> GetFacturas()
        {
            try
            {
                var facturas = await _context.Facturas
                    .Include(f => f.Cliente)
                    .OrderByDescending(f => f.Fecha)
                    .ToListAsync();

                return Ok(facturas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener facturas");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        private string GenerarNumeroFactura()
        {
            var fecha = DateTime.Now;
            var random = new Random();
            return $"FACT-{fecha:yyyyMMdd}-{random.Next(1000, 9999)}";
        }
    }
}
