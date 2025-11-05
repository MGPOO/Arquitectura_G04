using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ConUni_Soap_Dotnet_CliWeb_G04.Services;

namespace ConUni_Soap_Dotnet_CliWeb_G04.Controllers
{
    [Authorize]
    public class ConversorController : Controller
    {
        private readonly ConversorSoapClient _api;
        public ConversorController(ConversorSoapClient api) => _api = api;

        [HttpGet]
        public IActionResult Index() => View();

        // ===================== LONGITUD =====================
        [HttpPost]
        public async Task<IActionResult> ConvertLongitud(double valor, string de, string a)
        {
            if (!ModelState.IsValid) return View("Index");
            if (valor <= 0) return Error("Ingresa un número válido mayor que 0 para Longitud.");

            double r;
            switch ($"{de}->{a}")
            {
                case "Centímetros->Pies": r = await _api.CentimetrosAPiesAsync(valor); break;
                case "Pies->Centímetros": r = await _api.PiesACentimetrosAsync(valor); break;
                case "Pulgadas->Centímetros": r = await _api.PulgadasACentimetrosAsync(valor); break;
                case "Centímetros->Pulgadas": r = await _api.CentimetrosAPulgadasAsync(valor); break;
                case "Metros->Yardas": r = await _api.MetrosAYardasAsync(valor); break;
                case "Yardas->Metros": r = await _api.YardasAMetrosAsync(valor); break;
                default:
                    ViewBag.Error = "Conversión de Longitud no soportada.";
                    return View("Index");
            }

            ViewBag.Longitud = $"{valor} {Simbolo(de)} = {r:F4} {Simbolo(a)}";
            return View("Index");
        }

        // ======================= MASA =======================
        [HttpPost]
        public async Task<IActionResult> ConvertMasa(double valor, string de, string a)
        {
            if (!ModelState.IsValid) return View("Index");
            if (valor < 0) return Error("La masa no puede ser negativa.");

            double r;
            switch ($"{de}->{a}")
            {
                case "Kilogramos->Libras": r = await _api.KgALibrasAsync(valor); break;
                case "Libras->Kilogramos": r = await _api.LibrasAKgAsync(valor); break;
                case "Gramos->Onzas": r = await _api.GramosAOnzasAsync(valor); break;
                case "Onzas->Gramos": r = await _api.OnzasAGramosAsync(valor); break;
                default:
                    ViewBag.Error = "Conversión de Masa no soportada.";
                    return View("Index");
            }

            ViewBag.Masa = $"{valor} {Simbolo(de)} = {r:F4} {Simbolo(a)}";
            return View("Index");
        }

        // ==================== TEMPERATURA ====================
        [HttpPost]
        public async Task<IActionResult> ConvertTemperatura(double valor, string de, string a)
        {
            // En temperatura pueden existir negativos, no forzamos > 0
            double r;
            switch ($"{de}->{a}")
            {
                case "Celsius->Fahrenheit": r = await _api.CelsiusAFahrenheitAsync(valor); break;
                case "Fahrenheit->Celsius": r = await _api.FahrenheitACelsiusAsync(valor); break;
                case "Celsius->Kelvin": r = await _api.CelsiusAKelvinAsync(valor); break;
                case "Kelvin->Celsius": r = await _api.KelvinACelsiusAsync(valor); break;
                case "Fahrenheit->Kelvin": r = await _api.FahrenheitAKelvinAsync(valor); break;
                case "Kelvin->Fahrenheit": r = await _api.KelvinAFahrenheitAsync(valor); break;
                default:
                    ViewBag.Error = "Conversión de Temperatura no soportada.";
                    return View("Index");
            }

            ViewBag.Temperatura = $"{valor} {Simbolo(de)} = {r:F2} {Simbolo(a)}";
            return View("Index");
        }

        // ===================== Helpers ======================
        private IActionResult Error(string msg)
        {
            ViewBag.Error = msg;
            return View("Index");
        }

        private static string Simbolo(string unidad) => unidad switch
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

            _ => unidad
        };
    }
}
