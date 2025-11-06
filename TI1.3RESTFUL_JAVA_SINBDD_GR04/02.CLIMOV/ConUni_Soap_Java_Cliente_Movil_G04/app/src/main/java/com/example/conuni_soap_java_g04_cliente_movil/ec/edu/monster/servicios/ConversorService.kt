package com.example.conuni_soap_java_g04_cliente_movil.ec.edu.monster.servicios


import com.example.conuni_soap_java_g04_cliente_movil.ec.edu.monster.modelo.Unidad
import com.example.conuni_soap_java_g04_cliente_movil.ec.edu.monster.servicios.soap.ConversorSoapClient
import java.util.Locale

class ConversorService(
    private val soap: ConversorSoapClient = ConversorSoapClient()
) {
    fun longitudes(valor: Double, de: Unidad, a: Unidad) =
        format(valor, de, soap.convertirLongitud(valor, de, a), a)

    fun masas(valor: Double, de: Unidad, a: Unidad) =
        format(valor, de, soap.convertirMasa(valor, de, a), a)

    fun temperaturas(valor: Double, de: Unidad, a: Unidad) =
        format(valor, de, soap.convertirTemperatura(valor, de, a), a)

    private fun format(v: Double, de: Unidad, r: Double, a: Unidad): String {
        fun lbl(u: Unidad) = when (u) {
            Unidad.METROS -> "Metros"; Unidad.KILOMETROS -> "Kilómetros"
            Unidad.MILLAS -> "Millas"; Unidad.YARDAS -> "Yardas"
            Unidad.GRAMOS -> "Gramos"; Unidad.KILOGRAMOS -> "Kilogramos"
            Unidad.LIBRAS -> "Libras"; Unidad.ONZAS -> "Onzas"
            Unidad.CELSIUS -> "°C"; Unidad.FAHRENHEIT -> "°F"; Unidad.KELVIN -> "K"
        }
        fun num(x: Double) = String.format(Locale.US, "%.4f", x)
        return "${num(v)} ${lbl(de)} = ${num(r)} ${lbl(a)}"
    }
}