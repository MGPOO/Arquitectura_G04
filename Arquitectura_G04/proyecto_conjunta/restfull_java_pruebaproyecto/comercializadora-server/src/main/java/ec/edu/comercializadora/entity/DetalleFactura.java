package ec.edu.comercializadora.entity;

import jakarta.persistence.*;
import java.io.Serializable;
import java.math.BigDecimal;

@Entity
@Table(name = "DETALLE_FACTURA")
public class DetalleFactura implements Serializable {
    
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id_detalle")
    private Integer idDetalle;
    
    @ManyToOne
    @JoinColumn(name = "id_factura", nullable = false)
    private Factura factura;
    
    @ManyToOne
    @JoinColumn(name = "id_producto", nullable = false)
    private Producto producto;
    
    @Column(name = "cantidad", nullable = false)
    private Integer cantidad;
    
    @Column(name = "precio_unitario", nullable = false, precision = 18, scale = 2)
    private BigDecimal precioUnitario;
    
    @Column(name = "total_linea", nullable = false, precision = 18, scale = 2)
    private BigDecimal totalLinea;
    
    public DetalleFactura() {}
    
    // Getters y Setters
    public Integer getIdDetalle() {
        return idDetalle;
    }
    
    public void setIdDetalle(Integer idDetalle) {
        this.idDetalle = idDetalle;
    }
    
    public Factura getFactura() {
        return factura;
    }
    
    public void setFactura(Factura factura) {
        this.factura = factura;
    }
    
    public Producto getProducto() {
        return producto;
    }
    
    public void setProducto(Producto producto) {
        this.producto = producto;
    }
    
    public Integer getCantidad() {
        return cantidad;
    }
    
    public void setCantidad(Integer cantidad) {
        this.cantidad = cantidad;
    }
    
    public BigDecimal getPrecioUnitario() {
        return precioUnitario;
    }
    
    public void setPrecioUnitario(BigDecimal precioUnitario) {
        this.precioUnitario = precioUnitario;
    }
    
    public BigDecimal getTotalLinea() {
        return totalLinea;
    }
    
    public void setTotalLinea(BigDecimal totalLinea) {
        this.totalLinea = totalLinea;
    }
}
