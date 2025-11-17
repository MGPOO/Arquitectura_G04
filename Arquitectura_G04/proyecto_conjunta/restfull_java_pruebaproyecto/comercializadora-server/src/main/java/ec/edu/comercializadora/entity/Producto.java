package ec.edu.comercializadora.entity;

import jakarta.persistence.*;
import java.io.Serializable;
import java.math.BigDecimal;
import java.util.List;

@Entity
@Table(name = "PRODUCTO")
public class Producto implements Serializable {
    
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id_producto")
    private Integer idProducto;
    
    @Column(name = "codigo", nullable = false, unique = true, length = 20)
    private String codigo;
    
    @Column(name = "nombre", nullable = false, length = 100)
    private String nombre;
    
    @Column(name = "descripcion", length = 255)
    private String descripcion;
    
    @Column(name = "precio_venta", nullable = false, precision = 18, scale = 2)
    private BigDecimal precioVenta;
    
    @OneToMany(mappedBy = "producto", cascade = CascadeType.ALL)
    private List<DetalleFactura> detalles;
    
    public Producto() {}
    
    // Getters y Setters
    public Integer getIdProducto() {
        return idProducto;
    }
    
    public void setIdProducto(Integer idProducto) {
        this.idProducto = idProducto;
    }
    
    public String getCodigo() {
        return codigo;
    }
    
    public void setCodigo(String codigo) {
        this.codigo = codigo;
    }
    
    public String getNombre() {
        return nombre;
    }
    
    public void setNombre(String nombre) {
        this.nombre = nombre;
    }
    
    public String getDescripcion() {
        return descripcion;
    }
    
    public void setDescripcion(String descripcion) {
        this.descripcion = descripcion;
    }
    
    public BigDecimal getPrecioVenta() {
        return precioVenta;
    }
    
    public void setPrecioVenta(BigDecimal precioVenta) {
        this.precioVenta = precioVenta;
    }
    
    public List<DetalleFactura> getDetalles() {
        return detalles;
    }
    
    public void setDetalles(List<DetalleFactura> detalles) {
        this.detalles = detalles;
    }
}
