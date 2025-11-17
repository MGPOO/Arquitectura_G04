package ec.edu.banquito.rest;

import jakarta.ws.rs.ApplicationPath;
import jakarta.ws.rs.core.Application;

@ApplicationPath("/api")
public class RestApplication extends Application {
    // La configuración de JAX-RS se realiza automáticamente
}
