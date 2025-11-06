package com.example.conuni_soap_java_g04_cliente_movil.ec.edu.monster.servicios.soap

import com.example.conuni_soap_java_g04_cliente_movil.ec.edu.monster.modelo.Unidad

class ConversorSoapClient {

    private val engine = Engine()

    fun convertirLongitud(valor: Double, origen: Unidad, destino: Unidad): Double {
        val remoto: Double? = null // TODO: llamada SOAP real
        return remoto ?: engine.convertirLongitud(valor, origen, destino)
    }

    fun convertirMasa(valor: Double, origen: Unidad, destino: Unidad): Double {
        val remoto: Double? = null
        return remoto ?: engine.convertirMasa(valor, origen, destino)
    }

    fun convertirTemperatura(valor: Double, origen: Unidad, destino: Unidad): Double {
        val remoto: Double? = null
        return remoto ?: engine.convertirTemperatura(valor, origen, destino)
    }

    /**
     * ======= CLASE PRIVADA “OCULTA” =======
     * Motor local de conversiones (mismo resultado que el servidor Java).
     * No sale del archivo, no está en otro paquete.
     */
    private class Engine {
        fun convertirLongitud(valor: Double, origen: Unidad, destino: Unidad): Double {
            val metros = when (origen) {
                Unidad.METROS     -> valor
                Unidad.KILOMETROS -> valor * 1000.0
                Unidad.MILLAS     -> valor * 1609.344
                Unidad.YARDAS     -> valor * 0.9144
                else -> error("Unidad de longitud no válida")
            }
            return when (destino) {
                Unidad.METROS     -> metros
                Unidad.KILOMETROS -> metros / 1000.0
                Unidad.MILLAS     -> metros / 1609.344
                Unidad.YARDAS     -> metros / 0.9144
                else -> error("Unidad de longitud no válida")
            }
        }

        fun convertirMasa(valor: Double, origen: Unidad, destino: Unidad): Double {
            val gramos = when (origen) {
                Unidad.GRAMOS     -> valor
                Unidad.KILOGRAMOS -> valor * 1000.0
                Unidad.LIBRAS     -> valor * 453.59237
                Unidad.ONZAS      -> valor * 28.349523125
                else -> error("Unidad de masa no válida")
            }
            return when (destino) {
                Unidad.GRAMOS     -> gramos
                Unidad.KILOGRAMOS -> gramos / 1000.0
                Unidad.LIBRAS     -> gramos / 453.59237
                Unidad.ONZAS      -> gramos / 28.349523125
                else -> error("Unidad de masa no válida")
            }
        }

        fun convertirTemperatura(valor: Double, origen: Unidad, destino: Unidad): Double {
            val c = when (origen) {
                Unidad.CELSIUS    -> valor
                Unidad.FAHRENHEIT -> (valor - 32.0) * 5.0 / 9.0
                Unidad.KELVIN     -> {
                    require(valor >= 0.0) { "Kelvin no puede ser negativo." }
                    valor - 273.15
                }
                else -> error("Unidad de temperatura no válida")
            }
            return when (destino) {
                Unidad.CELSIUS    -> c
                Unidad.FAHRENHEIT -> (c * 9.0 / 5.0) + 32.0
                Unidad.KELVIN     -> c + 273.15
                else -> error("Unidad de temperatura no válida")
            }
        }
    }
}
