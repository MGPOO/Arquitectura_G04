package ec.edu.banquito.dto;

import java.io.Serializable;
import java.math.BigDecimal;
import java.time.LocalDate;

public class CuotaCreditoDTO implements Serializable {
    
    private Integer numeroCuota;
    private LocalDate fechaVencimiento;
    private BigDecimal valorCuota;
    private BigDecimal interesPagado;
    private BigDecimal capitalPagado;
    private BigDecimal saldoRestante;
    
    public CuotaCreditoDTO() {}
    
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
