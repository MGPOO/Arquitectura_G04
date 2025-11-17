package ec.edu.banquito.entity;

import jakarta.persistence.*;
import java.io.Serializable;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;

@Entity
@Table(name = "CREDITO")
public class Credito implements Serializable {
    
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id_credito")
    private Integer idCredito;
    
    @ManyToOne
    @JoinColumn(name = "id_cliente", nullable = false)
    private Cliente cliente;
    
    @ManyToOne
    @JoinColumn(name = "id_cuenta", nullable = false)
    private Cuenta cuenta;
    
    @Column(name = "monto_credito", nullable = false, precision = 18, scale = 2)
    private BigDecimal montoCredito;
    
    @Column(name = "plazo_meses", nullable = false)
    private Integer plazoMeses;
    
    @Column(name = "tasa_anual", nullable = false, precision = 5, scale = 2)
    private BigDecimal tasaAnual = new BigDecimal("16.00");
    
    @Column(name = "fecha_inicio", nullable = false)
    private LocalDate fechaInicio;
    
    @Column(name = "estado", nullable = false, length = 20)
    private String estado;
    
    @OneToMany(mappedBy = "credito", cascade = CascadeType.ALL)
    private List<CuotaCredito> cuotas;
    
    public Credito() {}
    
    // Getters y Setters
    public Integer getIdCredito() {
        return idCredito;
    }
    
    public void setIdCredito(Integer idCredito) {
        this.idCredito = idCredito;
    }
    
    public Cliente getCliente() {
        return cliente;
    }
    
    public void setCliente(Cliente cliente) {
        this.cliente = cliente;
    }
    
    public Cuenta getCuenta() {
        return cuenta;
    }
    
    public void setCuenta(Cuenta cuenta) {
        this.cuenta = cuenta;
    }
    
    public BigDecimal getMontoCredito() {
        return montoCredito;
    }
    
    public void setMontoCredito(BigDecimal montoCredito) {
        this.montoCredito = montoCredito;
    }
    
    public Integer getPlazoMeses() {
        return plazoMeses;
    }
    
    public void setPlazoMeses(Integer plazoMeses) {
        this.plazoMeses = plazoMeses;
    }
    
    public BigDecimal getTasaAnual() {
        return tasaAnual;
    }
    
    public void setTasaAnual(BigDecimal tasaAnual) {
        this.tasaAnual = tasaAnual;
    }
    
    public LocalDate getFechaInicio() {
        return fechaInicio;
    }
    
    public void setFechaInicio(LocalDate fechaInicio) {
        this.fechaInicio = fechaInicio;
    }
    
    public String getEstado() {
        return estado;
    }
    
    public void setEstado(String estado) {
        this.estado = estado;
    }
    
    public List<CuotaCredito> getCuotas() {
        return cuotas;
    }
    
    public void setCuotas(List<CuotaCredito> cuotas) {
        this.cuotas = cuotas;
    }
}
