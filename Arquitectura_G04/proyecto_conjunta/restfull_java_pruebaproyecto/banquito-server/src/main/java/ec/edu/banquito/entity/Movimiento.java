package ec.edu.banquito.entity;

import jakarta.persistence.*;
import java.io.Serializable;
import java.math.BigDecimal;
import java.time.LocalDateTime;

@Entity
@Table(name = "MOVIMIENTO")
public class Movimiento implements Serializable {
    
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id_movimiento")
    private Integer idMovimiento;
    
    @ManyToOne
    @JoinColumn(name = "id_cuenta", nullable = false)
    private Cuenta cuenta;
    
    @Column(name = "fecha_movimiento", nullable = false)
    private LocalDateTime fechaMovimiento;
    
    @Column(name = "tipo_movimiento", nullable = false, length = 20)
    private String tipoMovimiento;
    
    @Column(name = "monto", nullable = false, precision = 18, scale = 2)
    private BigDecimal monto;
    
    @Column(name = "descripcion", length = 255)
    private String descripcion;
    
    public Movimiento() {}
    
    // Getters y Setters
    public Integer getIdMovimiento() {
        return idMovimiento;
    }
    
    public void setIdMovimiento(Integer idMovimiento) {
        this.idMovimiento = idMovimiento;
    }
    
    public Cuenta getCuenta() {
        return cuenta;
    }
    
    public void setCuenta(Cuenta cuenta) {
        this.cuenta = cuenta;
    }
    
    public LocalDateTime getFechaMovimiento() {
        return fechaMovimiento;
    }
    
    public void setFechaMovimiento(LocalDateTime fechaMovimiento) {
        this.fechaMovimiento = fechaMovimiento;
    }
    
    public String getTipoMovimiento() {
        return tipoMovimiento;
    }
    
    public void setTipoMovimiento(String tipoMovimiento) {
        this.tipoMovimiento = tipoMovimiento;
    }
    
    public BigDecimal getMonto() {
        return monto;
    }
    
    public void setMonto(BigDecimal monto) {
        this.monto = monto;
    }
    
    public String getDescripcion() {
        return descripcion;
    }
    
    public void setDescripcion(String descripcion) {
        this.descripcion = descripcion;
    }
}
