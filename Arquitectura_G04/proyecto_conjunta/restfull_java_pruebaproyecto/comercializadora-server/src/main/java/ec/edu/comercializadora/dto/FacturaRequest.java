package ec.edu.comercializadora.dto;

import java.io.Serializable;
import java.util.List;

public class FacturaRequest implements Serializable {
    
    private String cedula;
    private String formaPago; // EFECTIVO o CREDITO
    private Integer plazoMeses; // Solo para CREDITO
    private List<DetalleFacturaRequest> detalles;
    
    public FacturaRequest() {}
    
    public String getCedula() {
        return cedula;
    }
    
    public void setCedula(String cedula) {
        this.cedula = cedula;
    }
    
    public String getFormaPago() {
        return formaPago;
    }
    
    public void setFormaPago(String formaPago) {
        this.formaPago = formaPago;
    }
    
    public Integer getPlazoMeses() {
        return plazoMeses;
    }
    
    public void setPlazoMeses(Integer plazoMeses) {
        this.plazoMeses = plazoMeses;
    }
    
    public List<DetalleFacturaRequest> getDetalles() {
        return detalles;
    }
    
    public void setDetalles(List<DetalleFacturaRequest> detalles) {
        this.detalles = detalles;
    }
}
