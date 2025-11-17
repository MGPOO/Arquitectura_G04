package ec.edu.banquito.dto;

import java.io.Serializable;
import java.math.BigDecimal;

public class EvaluacionCreditoRequest implements Serializable {
    
    private String cedula;
    private BigDecimal precioElectrodomestico;
    private Integer plazoMeses;
    
    public EvaluacionCreditoRequest() {}
    
    public String getCedula() {
        return cedula;
    }
    
    public void setCedula(String cedula) {
        this.cedula = cedula;
    }
    
    public BigDecimal getPrecioElectrodomestico() {
        return precioElectrodomestico;
    }
    
    public void setPrecioElectrodomestico(BigDecimal precioElectrodomestico) {
        this.precioElectrodomestico = precioElectrodomestico;
    }
    
    public Integer getPlazoMeses() {
        return plazoMeses;
    }
    
    public void setPlazoMeses(Integer plazoMeses) {
        this.plazoMeses = plazoMeses;
    }
}
