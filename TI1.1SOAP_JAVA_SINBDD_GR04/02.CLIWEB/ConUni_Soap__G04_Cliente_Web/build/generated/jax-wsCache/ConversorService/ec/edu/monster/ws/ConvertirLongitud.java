
package ec.edu.monster.ws;

import jakarta.xml.bind.annotation.XmlAccessType;
import jakarta.xml.bind.annotation.XmlAccessorType;
import jakarta.xml.bind.annotation.XmlType;


/**
 * <p>Clase Java para convertirLongitud complex type.</p>
 * 
 * <p>El siguiente fragmento de esquema especifica el contenido que se espera que haya en esta clase.</p>
 * 
 * <pre>{@code
 * <complexType name="convertirLongitud">
 *   <complexContent>
 *     <restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       <sequence>
 *         <element name="valor" type="{http://www.w3.org/2001/XMLSchema}double"/>
 *         <element name="unidadOrigen" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *         <element name="unidadDestino" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *       </sequence>
 *     </restriction>
 *   </complexContent>
 * </complexType>
 * }</pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "convertirLongitud", propOrder = {
    "valor",
    "unidadOrigen",
    "unidadDestino"
})
public class ConvertirLongitud {

    protected double valor;
    protected String unidadOrigen;
    protected String unidadDestino;

    /**
     * Obtiene el valor de la propiedad valor.
     * 
     */
    public double getValor() {
        return valor;
    }

    /**
     * Define el valor de la propiedad valor.
     * 
     */
    public void setValor(double value) {
        this.valor = value;
    }

    /**
     * Obtiene el valor de la propiedad unidadOrigen.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getUnidadOrigen() {
        return unidadOrigen;
    }

    /**
     * Define el valor de la propiedad unidadOrigen.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setUnidadOrigen(String value) {
        this.unidadOrigen = value;
    }

    /**
     * Obtiene el valor de la propiedad unidadDestino.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getUnidadDestino() {
        return unidadDestino;
    }

    /**
     * Define el valor de la propiedad unidadDestino.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setUnidadDestino(String value) {
        this.unidadDestino = value;
    }

}
