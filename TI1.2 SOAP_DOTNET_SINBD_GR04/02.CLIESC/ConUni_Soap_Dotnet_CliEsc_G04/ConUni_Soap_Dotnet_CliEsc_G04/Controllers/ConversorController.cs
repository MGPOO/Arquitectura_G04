using System;
using System.Threading.Tasks;
using ConUni_Soap_Dotnet_CliEsc_G04.Services;
using ConversorRef; // proxy WCF

namespace ConUni_Soap_Dotnet_CliEsc_G04.Controllers
{
    public class ConversorController
    {
        // --------------------- LONGITUD ---------------------
        public async Task<string> ConvertirLongitudAsync(double valor, string de, string a)
        {
            if (valor <= 0) return "Ingresa un número > 0.";

            var api = SoapClientFactory.Create();
            try
            {
                await api.OpenAsync();

                double r = (de, a) switch
                {
                    ("Centímetros", "Pies") => await api.CentimetersToFeetAsync(valor),
                    ("Pies", "Centímetros") => await api.FeetToCentimetersAsync(valor),
                    ("Pulgadas", "Centímetros") => await api.InchesToCentimetersAsync(valor),
                    ("Centímetros", "Pulgadas") => await api.CentimetersToInchesAsync(valor),
                    ("Metros", "Yardas") => await api.MetersToYardsAsync(valor),
                    ("Yardas", "Metros") => await api.YardsToMetersAsync(valor),
                    _ => throw new NotSupportedException("Conversión no soportada.")
                };

                // NO Close/Abort en tu proxy
                return $"{valor} {Simbolo(de)} = {r:F4} {Simbolo(a)}";
            }
            catch (Exception ex)
            {
                return "Error SOAP: " + ex.Message;
            }
        }

        // ----------------------- MASA -----------------------
        public async Task<string> ConvertirMasaAsync(double valor, string de, string a)
        {
            if (valor < 0) return "La masa no puede ser negativa.";

            var api = SoapClientFactory.Create();
            try
            {
                await api.OpenAsync();

                double r = (de, a) switch
                {
                    ("Kilogramos", "Libras") => await api.KilogramsToPoundsAsync(valor),
                    ("Libras", "Kilogramos") => await api.PoundsToKilogramsAsync(valor),
                    ("Gramos", "Onzas") => await api.GramsToOuncesAsync(valor),
                    ("Onzas", "Gramos") => await api.OuncesToGramsAsync(valor),
                    _ => throw new NotSupportedException("Conversión no soportada.")
                };

                return $"{valor} {Simbolo(de)} = {r:F4} {Simbolo(a)}";
            }
            catch (Exception ex)
            {
                return "Error SOAP: " + ex.Message;
            }
        }

        // -------------------- TEMPERATURA --------------------
        public async Task<string> ConvertirTemperaturaAsync(double valor, string de, string a)
        {
            var api = SoapClientFactory.Create();
            try
            {
                await api.OpenAsync();

                double r = (de, a) switch
                {
                    ("Celsius", "Fahrenheit") => await api.CelsiusToFahrenheitAsync(valor),
                    ("Fahrenheit", "Celsius") => await api.FahrenheitToCelsiusAsync(valor),
                    ("Celsius", "Kelvin") => await api.CelsiusToKelvinAsync(valor),
                    ("Kelvin", "Celsius") => await api.KelvinToCelsiusAsync(valor),
                    ("Fahrenheit", "Kelvin") => await api.FahrenheitToKelvinAsync(valor),
                    ("Kelvin", "Fahrenheit") => await api.KelvinToFahrenheitAsync(valor),
                    _ => throw new NotSupportedException("Conversión no soportada.")
                };

                return $"{valor} {Simbolo(de)} = {r:F2} {Simbolo(a)}";
            }
            catch (Exception ex)
            {
                return "Error SOAP: " + ex.Message;
            }
        }

        // ----------------------- Helper ----------------------
        private static string Simbolo(string u) => u switch
        {
            // Longitud
            "Centímetros" => "cm",
            "Pulgadas" => "in",
            "Pies" => "ft",
            "Metros" => "m",
            "Yardas" => "yd",
            // Masa
            "Kilogramos" => "kg",
            "Libras" => "lb",
            "Gramos" => "g",
            "Onzas" => "oz",
            // Temperatura
            "Celsius" => "°C",
            "Fahrenheit" => "°F",
            "Kelvin" => "K",
            _ => u
        };
    }
}
