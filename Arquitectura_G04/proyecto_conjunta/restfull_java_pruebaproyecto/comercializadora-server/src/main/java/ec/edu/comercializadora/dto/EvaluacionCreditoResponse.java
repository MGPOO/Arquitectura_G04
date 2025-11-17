package ec.edu.comercializadora.dto;

import java.io.Serializable;
import java.math.BigDecimal;

public class EvaluacionCreditoResponse implements Serializable {
    
    private boolean sujetoCredito;
    private BigDecimal montoMaximo;
    private boolean aprobado;
    private Integer idCredito;
    private String motivo;
    
    public EvaluacionCreditoResponse() {}
    
    public boolean isSujetoCredito() {
        return sujetoCredito;
    }
    
    public void setSujetoCredito(boolean sujetoCredito) {
        this.sujetoCredito = sujetoCredito;
    }
    
    public BigDecimal getMontoMaximo() {
        return montoMaximo;
    }
    
    public void setMontoMaximo(BigDecimal montoMaximo) {
        this.montoMaximo = montoMaximo;
    }
    
    public boolean isAprobado() {
        return aprobado;
    }
    
    public void setAprobado(boolean aprobado) {
        this.aprobado = aprobado;
    }
    
    public Integer getIdCredito() {
        return idCredito;
    }
    
    public void setIdCredito(Integer idCredito) {
        this.idCredito = idCredito;
    }
    
    public String getMotivo() {
        return motivo;
    }
    
    public void setMotivo(String motivo) {
        this.motivo = motivo;
    }
}
