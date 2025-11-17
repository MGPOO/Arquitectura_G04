package ec.edu.comercializadora.dto;

import java.io.Serializable;

public class DetalleFacturaRequest implements Serializable {
    
    private Integer idProducto;
    private Integer cantidad;
    
    public DetalleFacturaRequest() {}
    
    public Integer getIdProducto() {
        return idProducto;
    }
    
    public void setIdProducto(Integer idProducto) {
        this.idProducto = idProducto;
    }
    
    public Integer getCantidad() {
        return cantidad;
    }
    
    public void setCantidad(Integer cantidad) {
        this.cantidad = cantidad;
    }
}
