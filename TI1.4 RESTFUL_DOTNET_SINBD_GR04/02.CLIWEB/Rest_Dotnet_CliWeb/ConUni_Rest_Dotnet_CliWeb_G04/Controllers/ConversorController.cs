using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ConUni_Rest_Dotnet_CliWeb_G04.Services;

namespace ConUni_Rest_Dotnet_CliWeb_G04.Controllers
{
    [Authorize]
    public class ConversorController : Controller
    {
        private readonly ConversorApi _api;
        public ConversorController(ConversorApi api) => _api = api;

        private static string Key(string cat, string de, string a)
            => $"{cat}|{de}|{a}";

        private static readonly Dictionary<string, string> Map =
            new(StringComparer.OrdinalIgnoreCase)
            {
                // Longitud
                [Key("longitud", "Centímetros", "Pies")] = "centimetros-a-pies",
                [Key("longitud", "Pies", "Centímetros")] = "pies-a-centimetros",
                [Key("longitud", "Pulgadas", "Centímetros")] = "pulgadas-a-centimetros",
                [Key("longitud", "Centímetros", "Pulgadas")] = "centimetros-a-pulgadas",
                [Key("longitud", "Metros", "Yardas")] = "metros-a-yardas",
                [Key("longitud", "Yardas", "Metros")] = "yardas-a-metros",

                // Masa
                [Key("masa", "Kilogramos", "Libras")] = "kilogramos-a-libras",
                [Key("masa", "Libras", "Kilogramos")] = "libras-a-kilogramos",
                [Key("masa", "Gramos", "Onzas")] = "gramos-a-onzas",
                [Key("masa", "Onzas", "Gramos")] = "onzas-a-gramos",

                // Temperatura
                [Key("temperatura", "Celsius", "Fahrenheit")] = "celsius-a-fahrenheit",
                [Key("temperatura", "Fahrenheit", "Celsius")] = "fahrenheit-a-celsius",
                [Key("temperatura", "Celsius", "Kelvin")] = "celsius-a-kelvin",
                [Key("temperatura", "Kelvin", "Celsius")] = "kelvin-a-celsius",
                [Key("temperatura", "Fahrenheit", "Kelvin")] = "fahrenheit-a-kelvin",
                [Key("temperatura", "Kelvin", "Fahrenheit")] = "kelvin-a-fahrenheit",
            };

        [HttpGet]
        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> Convert(string categoria, string de, string a, double valor)
        {
            if (string.Equals(de, a, StringComparison.OrdinalIgnoreCase))
            {
                TempData["Error"] = "Las unidades 'De' y 'A' no pueden ser iguales.";
                return RedirectToAction(nameof(Index));
            }

            var allowNegative =
                string.Equals(categoria, "temperatura", StringComparison.OrdinalIgnoreCase);

            if (!allowNegative && valor < 0)
            {
                TempData["Error"] =
                    "Sólo se permiten valores numéricos >= 0 para Longitud y Masa.";
                return RedirectToAction(nameof(Index));
            }

            var k = Key(categoria, de, a);
            if (!Map.TryGetValue(k, out var endpoint))
            {
                TempData["Error"] = "Conversión no soportada.";
                return RedirectToAction(nameof(Index));
            }

            var (ok, data, error) = await _api.ConvertAsync(endpoint, valor);
            if (!ok || data is null)
            {
                TempData["Error"] = $"Error de API: {error}";
            }
            else
            {
                TempData["Ok"] =
                    $"{data.DeUnidad} → {data.AUnidad}\nEntrada: {data.ValorIngresado}\nResultado: {data.Resultado}";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
