package ec.edu.banquito.entity;

import jakarta.persistence.*;
import java.io.Serializable;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;

@Entity
@Table(name = "CUENTA")
public class Cuenta implements Serializable {
    
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id_cuenta")
    private Integer idCuenta;
    
    @Column(name = "numero_cuenta", nullable = false, unique = true, length = 20)
    private String numeroCuenta;
    
    @ManyToOne
    @JoinColumn(name = "id_cliente", nullable = false)
    private Cliente cliente;
    
    @Column(name = "tipo_cuenta", nullable = false, length = 20)
    private String tipoCuenta;
    
    @Column(name = "saldo", nullable = false, precision = 18, scale = 2)
    private BigDecimal saldo = BigDecimal.ZERO;
    
    @Column(name = "fecha_apertura", nullable = false)
    private LocalDate fechaApertura;
    
    @OneToMany(mappedBy = "cuenta", cascade = CascadeType.ALL)
    private List<Movimiento> movimientos;
    
    @OneToMany(mappedBy = "cuenta", cascade = CascadeType.ALL)
    private List<Credito> creditos;
    
    public Cuenta() {}
    
    // Getters y Setters
    public Integer getIdCuenta() {
        return idCuenta;
    }
    
    public void setIdCuenta(Integer idCuenta) {
        this.idCuenta = idCuenta;
    }
    
    public String getNumeroCuenta() {
        return numeroCuenta;
    }
    
    public void setNumeroCuenta(String numeroCuenta) {
        this.numeroCuenta = numeroCuenta;
    }
    
    public Cliente getCliente() {
        return cliente;
    }
    
    public void setCliente(Cliente cliente) {
        this.cliente = cliente;
    }
    
    public String getTipoCuenta() {
        return tipoCuenta;
    }
    
    public void setTipoCuenta(String tipoCuenta) {
        this.tipoCuenta = tipoCuenta;
    }
    
    public BigDecimal getSaldo() {
        return saldo;
    }
    
    public void setSaldo(BigDecimal saldo) {
        this.saldo = saldo;
    }
    
    public LocalDate getFechaApertura() {
        return fechaApertura;
    }
    
    public void setFechaApertura(LocalDate fechaApertura) {
        this.fechaApertura = fechaApertura;
    }
    
    public List<Movimiento> getMovimientos() {
        return movimientos;
    }
    
    public void setMovimientos(List<Movimiento> movimientos) {
        this.movimientos = movimientos;
    }
    
    public List<Credito> getCreditos() {
        return creditos;
    }
    
    public void setCreditos(List<Credito> creditos) {
        this.creditos = creditos;
    }
}
