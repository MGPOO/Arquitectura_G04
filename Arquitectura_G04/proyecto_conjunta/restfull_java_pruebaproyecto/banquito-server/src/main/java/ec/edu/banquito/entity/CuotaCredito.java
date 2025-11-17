package ec.edu.banquito.entity;

import jakarta.persistence.*;
import java.io.Serializable;
import java.math.BigDecimal;
import java.time.LocalDate;

@Entity
@Table(name = "CUOTA_CREDITO")
public class CuotaCredito implements Serializable {
    
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id_cuota")
    private Integer idCuota;
    
    @ManyToOne
    @JoinColumn(name = "id_credito", nullable = false)
    private Credito credito;
    
    @Column(name = "numero_cuota", nullable = false)
    private Integer numeroCuota;
    
    @Column(name = "fecha_vencimiento", nullable = false)
    private LocalDate fechaVencimiento;
    
    @Column(name = "valor_cuota", nullable = false, precision = 18, scale = 2)
    private BigDecimal valorCuota;
    
    @Column(name = "interes_pagado", nullable = false, precision = 18, scale = 2)
    private BigDecimal interesPagado;
    
    @Column(name = "capital_pagado", nullable = false, precision = 18, scale = 2)
    private BigDecimal capitalPagado;
    
    @Column(name = "saldo_restante", nullable = false, precision = 18, scale = 2)
    private BigDecimal saldoRestante;
    
    public CuotaCredito() {}
    
    // Getters y Setters
    public Integer getIdCuota() {
        return idCuota;
    }
    
    public void setIdCuota(Integer idCuota) {
        this.idCuota = idCuota;
    }
    
    public Credito getCredito() {
        return credito;
    }
    
    public void setCredito(Credito credito) {
        this.credito = credito;
    }
    
    public Integer getNumeroCuota() {
        return numeroCuota;
    }
    
    public void setNumeroCuota(Integer numeroCuota) {
        this.numeroCuota = numeroCuota;
    }
    
    public LocalDate getFechaVencimiento() {
        return fechaVencimiento;
    }
    
    public void setFechaVencimiento(LocalDate fechaVencimiento) {
        this.fechaVencimiento = fechaVencimiento;
    }
    
    public BigDecimal getValorCuota() {
        return valorCuota;
    }
    
    public void setValorCuota(BigDecimal valorCuota) {
        this.valorCuota = valorCuota;
    }
    
    public BigDecimal getInteresPagado() {
        return interesPagado;
    }
    
    public void setInteresPagado(BigDecimal interesPagado) {
        this.interesPagado = interesPagado;
    }
    
    public BigDecimal getCapitalPagado() {
        return capitalPagado;
    }
    
    public void setCapitalPagado(BigDecimal capitalPagado) {
        this.capitalPagado = capitalPagado;
    }
    
    public BigDecimal getSaldoRestante() {
        return saldoRestante;
    }
    
    public void setSaldoRestante(BigDecimal saldoRestante) {
        this.saldoRestante = saldoRestante;
    }
}
