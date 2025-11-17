package ec.edu.comercializadora.service;

import ec.edu.comercializadora.dto.*;
import ec.edu.comercializadora.entity.*;
import jakarta.ejb.Stateless;
import jakarta.inject.Inject;
import jakarta.persistence.EntityManager;
import jakarta.persistence.PersistenceContext;
import jakarta.persistence.TypedQuery;

import java.math.BigDecimal;
import java.math.RoundingMode;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.List;

@Stateless
public class FacturaService {
    
    @PersistenceContext(unitName = "ComercializadoraPU")
    private EntityManager em;
    
    @Inject
    private BancoRestClient bancoClient;
    
    public Factura crearFactura(FacturaRequest request) throws Exception {
        // 1. Buscar o crear cliente
        ClienteComercializadora cliente = buscarOCrearCliente(request.getCedula());
        
        // 2. Calcular subtotal
        BigDecimal subtotal = BigDecimal.ZERO;
        List<DetalleFactura> detalles = new ArrayList<>();
        
        for (DetalleFacturaRequest detReq : request.getDetalles()) {
            Producto producto = em.find(Producto.class, detReq.getIdProducto());
            if (producto == null) {
                throw new Exception("Producto no encontrado: " + detReq.getIdProducto());
            }
            
            BigDecimal totalLinea = producto.getPrecioVenta()
                .multiply(new BigDecimal(detReq.getCantidad()))
                .setScale(2, RoundingMode.HALF_UP);
            
            subtotal = subtotal.add(totalLinea);
            
            DetalleFactura detalle = new DetalleFactura();
            detalle.setProducto(producto);
            detalle.setCantidad(detReq.getCantidad());
            detalle.setPrecioUnitario(producto.getPrecioVenta());
            detalle.setTotalLinea(totalLinea);
            detalles.add(detalle);
        }
        
        // 3. Crear factura
        Factura factura = new Factura();
        factura.setCliente(cliente);
        factura.setFecha(LocalDateTime.now());
        factura.setFormaPago(request.getFormaPago());
        factura.setSubtotal(subtotal);
        
        BigDecimal descuento = BigDecimal.ZERO;
        BigDecimal total = subtotal;
        Integer idCreditoBanco = null;
        
        // 4. Procesar según forma de pago
        if ("EFECTIVO".equals(request.getFormaPago())) {
            // Descuento del 33%
            descuento = subtotal.multiply(new BigDecimal("0.33")).setScale(2, RoundingMode.HALF_UP);
            total = subtotal.subtract(descuento);
        } else if ("CREDITO".equals(request.getFormaPago())) {
            // Llamar al banco para evaluar crédito
            EvaluacionCreditoRequest creditoReq = new EvaluacionCreditoRequest();
            creditoReq.setCedula(request.getCedula());
            creditoReq.setPrecioElectrodomestico(subtotal);
            creditoReq.setPlazoMeses(request.getPlazoMeses());
            
            EvaluacionCreditoResponse creditoResp = bancoClient.evaluarCredito(creditoReq);
            
            if (!creditoResp.isAprobado()) {
                throw new Exception("Crédito rechazado: " + creditoResp.getMotivo());
            }
            
            idCreditoBanco = creditoResp.getIdCredito();
        } else {
            throw new Exception("Forma de pago no válida: " + request.getFormaPago());
        }
        
        factura.setDescuento(descuento);
        factura.setTotal(total);
        factura.setIdCreditoBanco(idCreditoBanco);
        
        // Generar número de factura
        String numeroFactura = generarNumeroFactura();
        factura.setNumeroFactura(numeroFactura);
        
        // 5. Persistir factura
        em.persist(factura);
        em.flush();
        
        // 6. Persistir detalles
        for (DetalleFactura detalle : detalles) {
            detalle.setFactura(factura);
            em.persist(detalle);
        }
        
        factura.setDetalles(detalles);
        
        return factura;
    }
    
    private ClienteComercializadora buscarOCrearCliente(String cedula) {
        TypedQuery<ClienteComercializadora> query = em.createQuery(
            "SELECT c FROM ClienteComercializadora c WHERE c.cedula = :cedula", 
            ClienteComercializadora.class);
        query.setParameter("cedula", cedula);
        List<ClienteComercializadora> clientes = query.getResultList();
        
        if (!clientes.isEmpty()) {
            return clientes.get(0);
        }
        
        // Crear nuevo cliente básico
        ClienteComercializadora cliente = new ClienteComercializadora();
        cliente.setCedula(cedula);
        cliente.setNombres("Cliente");
        cliente.setApellidos("Comercializadora");
        em.persist(cliente);
        em.flush();
        
        return cliente;
    }
    
    private String generarNumeroFactura() {
        DateTimeFormatter formatter = DateTimeFormatter.ofPattern("yyyyMMddHHmmss");
        return "FACT-" + LocalDateTime.now().format(formatter);
    }
    
    public List<Producto> listarProductos() {
        TypedQuery<Producto> query = em.createQuery(
            "SELECT p FROM Producto p", Producto.class);
        return query.getResultList();
    }
    
    public Factura obtenerFactura(Integer idFactura) {
        return em.find(Factura.class, idFactura);
    }
}
