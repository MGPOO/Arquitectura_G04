/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package ec.edu.monster.ws;

import jakarta.ws.rs.ApplicationPath;
import jakarta.ws.rs.core.Application;

/**
 *
 * @author Rabedon1
 */
//Registra todas las clases con anotaciones @Path (como ConvensorServicioREST) bajo la ruta base /api.”
//Servidor detecte tus recursos REST.
@ApplicationPath("api")
public class ApplicationConfig extends Application {
    
}
