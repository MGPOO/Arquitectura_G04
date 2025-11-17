package ec.edu.comercializadora.entity;

import jakarta.persistence.*;
import java.io.Serializable;
import java.util.List;

@Entity
@Table(name = "CLIENTE_COMERCIALIZADORA")
public class ClienteComercializadora implements Serializable {
    
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id_cliente")
    private Integer idCliente;
    
    @Column(name = "cedula", nullable = false, unique = true, length = 20)
    private String cedula;
    
    @Column(name = "nombres", nullable = false, length = 100)
    private String nombres;
    
    @Column(name = "apellidos", nullable = false, length = 100)
    private String apellidos;
    
    @Column(name = "direccion", length = 200)
    private String direccion;
    
    @Column(name = "telefono", length = 20)
    private String telefono;
    
    @Column(name = "email", length = 100)
    private String email;
    
    @OneToMany(mappedBy = "cliente", cascade = CascadeType.ALL)
    private List<Factura> facturas;
    
    public ClienteComercializadora() {}
    
    // Getters y Setters
    public Integer getIdCliente() {
        return idCliente;
    }
    
    public void setIdCliente(Integer idCliente) {
        this.idCliente = idCliente;
    }
    
    public String getCedula() {
        return cedula;
    }
    
    public void setCedula(String cedula) {
        this.cedula = cedula;
    }
    
    public String getNombres() {
        return nombres;
    }
    
    public void setNombres(String nombres) {
        this.nombres = nombres;
    }
    
    public String getApellidos() {
        return apellidos;
    }
    
    public void setApellidos(String apellidos) {
        this.apellidos = apellidos;
    }
    
    public String getDireccion() {
        return direccion;
    }
    
    public void setDireccion(String direccion) {
        this.direccion = direccion;
    }
    
    public String getTelefono() {
        return telefono;
    }
    
    public void setTelefono(String telefono) {
        this.telefono = telefono;
    }
    
    public String getEmail() {
        return email;
    }
    
    public void setEmail(String email) {
        this.email = email;
    }
    
    public List<Factura> getFacturas() {
        return facturas;
    }
    
    public void setFacturas(List<Factura> facturas) {
        this.facturas = facturas;
    }
}
