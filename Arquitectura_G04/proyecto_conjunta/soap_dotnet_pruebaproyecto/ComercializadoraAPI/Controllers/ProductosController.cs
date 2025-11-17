using Microsoft.AspNetCore.Mvc;
using ComercializadoraAPI.Data;
using ComercializadoraAPI.DTOs;
using ComercializadoraAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ComercializadoraAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly ComercializadoraDbContext _context;
        private readonly ILogger<ProductosController> _logger;

        public ProductosController(ComercializadoraDbContext context, ILogger<ProductosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> GetProductos()
        {
            try
            {
                var productos = await _context.Productos
                    .Select(p => new ProductoDto
                    {
                        IdProducto = p.IdProducto,
                        Codigo = p.Codigo,
                        Nombre = p.Nombre,
                        Descripcion = p.Descripcion,
                        PrecioVenta = p.PrecioVenta
                    })
                    .ToListAsync();

                return Ok(productos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener productos");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDto>> GetProducto(int id)
        {
            try
            {
                var producto = await _context.Productos
                    .Where(p => p.IdProducto == id)
                    .Select(p => new ProductoDto
                    {
                        IdProducto = p.IdProducto,
                        Codigo = p.Codigo,
                        Nombre = p.Nombre,
                        Descripcion = p.Descripcion,
                        PrecioVenta = p.PrecioVenta
                    })
                    .FirstOrDefaultAsync();

                if (producto == null)
                {
                    return NotFound("Producto no encontrado");
                }

                return Ok(producto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener producto");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProductoDto>> CreateProducto(CreateProductoDto createDto)
        {
            try
            {
                // Validar que el código no exista
                var existe = await _context.Productos.AnyAsync(p => p.Codigo == createDto.Codigo);
                if (existe)
                {
                    return BadRequest("Ya existe un producto con ese código");
                }

                var producto = new Producto
                {
                    Codigo = createDto.Codigo,
                    Nombre = createDto.Nombre,
                    Descripcion = createDto.Descripcion,
                    PrecioVenta = createDto.PrecioVenta
                };

                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();

                var productoDto = new ProductoDto
                {
                    IdProducto = producto.IdProducto,
                    Codigo = producto.Codigo,
                    Nombre = producto.Nombre,
                    Descripcion = producto.Descripcion,
                    PrecioVenta = producto.PrecioVenta
                };

                return CreatedAtAction(nameof(GetProducto), new { id = producto.IdProducto }, productoDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear producto");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
