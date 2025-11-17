package ec.edu.comercializadora.entity;

import jakarta.persistence.*;
import java.io.Serializable;
import java.math.BigDecimal;
import java.time.LocalDateTime;
import java.util.List;

@Entity
@Table(name = "FACTURA")
public class Factura implements Serializable {
    
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id_factura")
    private Integer idFactura;
    
    @Column(name = "numero_factura", nullable = false, unique = true, length = 30)
    private String numeroFactura;
    
    @ManyToOne
    @JoinColumn(name = "id_cliente", nullable = false)
    private ClienteComercializadora cliente;
    
    @Column(name = "fecha", nullable = false)
    private LocalDateTime fecha;
    
    @Column(name = "forma_pago", nullable = false, length = 20)
    private String formaPago;
    
    @Column(name = "subtotal", nullable = false, precision = 18, scale = 2)
    private BigDecimal subtotal;
    
    @Column(name = "descuento", nullable = false, precision = 18, scale = 2)
    private BigDecimal descuento = BigDecimal.ZERO;
    
    @Column(name = "total", nullable = false, precision = 18, scale = 2)
    private BigDecimal total;
    
    @Column(name = "id_credito_banco")
    private Integer idCreditoBanco;
    
    @OneToMany(mappedBy = "factura", cascade = CascadeType.ALL)
    private List<DetalleFactura> detalles;
    
    public Factura() {}
    
    // Getters y Setters
    public Integer getIdFactura() {
        return idFactura;
    }
    
    public void setIdFactura(Integer idFactura) {
        this.idFactura = idFactura;
    }
    
    public String getNumeroFactura() {
        return numeroFactura;
    }
    
    public void setNumeroFactura(String numeroFactura) {
        this.numeroFactura = numeroFactura;
    }
    
    public ClienteComercializadora getCliente() {
        return cliente;
    }
    
    public void setCliente(ClienteComercializadora cliente) {
        this.cliente = cliente;
    }
    
    public LocalDateTime getFecha() {
        return fecha;
    }
    
    public void setFecha(LocalDateTime fecha) {
        this.fecha = fecha;
    }
    
    public String getFormaPago() {
        return formaPago;
    }
    
    public void setFormaPago(String formaPago) {
        this.formaPago = formaPago;
    }
    
    public BigDecimal getSubtotal() {
        return subtotal;
    }
    
    public void setSubtotal(BigDecimal subtotal) {
        this.subtotal = subtotal;
    }
    
    public BigDecimal getDescuento() {
        return descuento;
    }
    
    public void setDescuento(BigDecimal descuento) {
        this.descuento = descuento;
    }
    
    public BigDecimal getTotal() {
        return total;
    }
    
    public void setTotal(BigDecimal total) {
        this.total = total;
    }
    
    public Integer getIdCreditoBanco() {
        return idCreditoBanco;
    }
    
    public void setIdCreditoBanco(Integer idCreditoBanco) {
        this.idCreditoBanco = idCreditoBanco;
    }
    
    public List<DetalleFactura> getDetalles() {
        return detalles;
    }
    
    public void setDetalles(List<DetalleFactura> detalles) {
        this.detalles = detalles;
    }
}
