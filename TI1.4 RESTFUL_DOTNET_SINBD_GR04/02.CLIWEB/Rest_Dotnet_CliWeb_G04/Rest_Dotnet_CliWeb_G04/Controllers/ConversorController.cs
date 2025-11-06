using System.Net.Http.Json;
using Cliente_Rest_G04.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cliente_Rest_G04.Controllers;

[Authorize]
public class ConversorController : Controller
{
    private readonly IHttpClientFactory _http;

    public ConversorController(IHttpClientFactory http) => _http = http;

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    // Un único endpoint para los 3 módulos. category: length|mass|temp
    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> Convertir(string category, string from, string to, double? valor)
    {
        if (valor is null || double.IsNaN(valor.Value) || double.IsInfinity(valor.Value))
        {
            ModelState.AddModelError("", "Valor inválido.");
            return View("Index");
        }

        var (ok, res) = await LlamarApi(category, from, to, valor.Value);

        if (!ok || res is null)
        {
            ModelState.AddModelError("", "Conversión no válida o error en API.");
            return View("Index");
        }

        // Texto estilo:  5 kg = 11.023 lb
        string texto = $"{res.ValorIngresado} {res.DeUnidad} = {res.Resultado} {res.AUnidad}";

        switch (category)
        {
            case "length":
                ViewBag.LengthResult = texto;
                break;
            case "mass":
                ViewBag.MassResult = texto;
                break;
            case "temp":
                ViewBag.TempResult = texto;
                break;
        }

        return View("Index");
    }

    private async Task<(bool ok, ConversionResponse? res)> LlamarApi(string category, string from, string to, double valor)
    {
        string? ruta = category switch
        {
            "length" => (from, to) switch
            {
                ("centimetros", "pies") => "centimetros-a-pies",
                ("pies", "centimetros") => "pies-a-centimetros",
                ("pulgadas", "centimetros") => "pulgadas-a-centimetros",
                ("centimetros", "pulgadas") => "centimetros-a-pulgadas",
                ("metros", "yardas") => "metros-a-yardas",
                ("yardas", "metros") => "yardas-a-metros",
                _ => null
            },
            "mass" => (from, to) switch
            {
                ("kilogramos", "libras") => "kilogramos-a-libras",
                ("libras", "kilogramos") => "libras-a-kilogramos",
                ("gramos", "onzas") => "gramos-a-onzas",
                ("onzas", "gramos") => "onzas-a-gramos",
                _ => null
            },
            "temp" => (from, to) switch
            {
                ("celsius", "fahrenheit") => "celsius-a-fahrenheit",
                ("fahrenheit", "celsius") => "fahrenheit-a-celsius",
                ("celsius", "kelvin") => "celsius-a-kelvin",
                ("kelvin", "celsius") => "kelvin-a-celsius",
                ("fahrenheit", "kelvin") => "fahrenheit-a-kelvin",
                ("kelvin", "fahrenheit") => "kelvin-a-fahrenheit",
                _ => null
            },
            _ => null
        };

        if (ruta is null)
            return (false, null);

        var cli = _http.CreateClient("ConversorApi");

        try
        {
            var res = await cli.GetFromJsonAsync<ConversionResponse>($"{ruta}?valor={valor}");
            return (res != null, res);
        }
        catch
        {
            return (false, null);
        }
    }
}
