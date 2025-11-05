using Microsoft.AspNetCore.Mvc;

namespace Servidor_Dotnet_REST_G04.Controllers;

[ApiController]
[Route("api/conversor")]
[Produces("application/json")]
public class ConversorController : ControllerBase
{
    // Respuesta consistente
    public sealed record ConversionResponse(
        string DeUnidad,
        string AUnidad,
        double ValorIngresado,
        double Resultado
    );

    // Helpers
    private static bool Invalido(double? v) =>
        v is null || double.IsNaN(v.Value) || double.IsInfinity(v.Value);

    private ActionResult Error(string msg) => Problem(
        title: "Entrada no válida",
        detail: msg,
        statusCode: 400
    );

    // ====================== LONGITUD ======================

    [HttpGet("centimetros-a-pies")]
    [ProducesResponseType(typeof(ConversionResponse), 200)]
    [ProducesResponseType(400)]
    public ActionResult<ConversionResponse> CentimetrosAPies([FromQuery] double? valor)
    {
        if (Invalido(valor) || valor! <= 0) return Error("El valor debe ser > 0 (centímetros).");
        double res = valor.Value / 30.48;
        return Ok(new ConversionResponse("centímetros", "pies", valor.Value, res));
    }

    [HttpGet("pies-a-centimetros")]
    [ProducesResponseType(typeof(ConversionResponse), 200)]
    [ProducesResponseType(400)]
    public ActionResult<ConversionResponse> PiesACentimetros([FromQuery] double? valor)
    {
        if (Invalido(valor) || valor! <= 0) return Error("El valor debe ser > 0 (pies).");
        double res = valor.Value * 30.48;
        return Ok(new ConversionResponse("pies", "centímetros", valor.Value, res));
    }

    [HttpGet("pulgadas-a-centimetros")]
    [ProducesResponseType(typeof(ConversionResponse), 200)]
    [ProducesResponseType(400)]
    public ActionResult<ConversionResponse> PulgadasACentimetros([FromQuery] double? valor)
    {
        if (Invalido(valor) || valor! <= 0) return Error("El valor debe ser > 0 (pulgadas).");
        double res = valor.Value * 2.54;
        return Ok(new ConversionResponse("pulgadas", "centímetros", valor.Value, res));
    }

    [HttpGet("centimetros-a-pulgadas")]
    [ProducesResponseType(typeof(ConversionResponse), 200)]
    [ProducesResponseType(400)]
    public ActionResult<ConversionResponse> CentimetrosAPulgadas([FromQuery] double? valor)
    {
        if (Invalido(valor) || valor! <= 0) return Error("El valor debe ser > 0 (centímetros).");
        double res = valor.Value / 2.54;
        return Ok(new ConversionResponse("centímetros", "pulgadas", valor.Value, res));
    }

    [HttpGet("metros-a-yardas")]
    [ProducesResponseType(typeof(ConversionResponse), 200)]
    [ProducesResponseType(400)]
    public ActionResult<ConversionResponse> MetrosAYardas([FromQuery] double? valor)
    {
        if (Invalido(valor) || valor! <= 0) return Error("El valor debe ser > 0 (metros).");
        double res = valor.Value * 1.0936133;
        return Ok(new ConversionResponse("metros", "yardas", valor.Value, res));
    }

    [HttpGet("yardas-a-metros")]
    [ProducesResponseType(typeof(ConversionResponse), 200)]
    [ProducesResponseType(400)]
    public ActionResult<ConversionResponse> YardasAMetros([FromQuery] double? valor)
    {
        if (Invalido(valor) || valor! <= 0) return Error("El valor debe ser > 0 (yardas).");
        double res = valor.Value / 1.0936133;
        return Ok(new ConversionResponse("yardas", "metros", valor.Value, res));
    }

    // ====================== MASA ======================

    [HttpGet("kilogramos-a-libras")]
    [ProducesResponseType(typeof(ConversionResponse), 200)]
    [ProducesResponseType(400)]
    public ActionResult<ConversionResponse> KilogramosALibras([FromQuery] double? valor)
    {
        if (Invalido(valor) || valor! < 0) return Error("El valor debe ser ≥ 0 (kilogramos).");
        double res = valor.Value * 2.2046226218;
        return Ok(new ConversionResponse("kilogramos", "libras", valor.Value, res));
    }

    [HttpGet("libras-a-kilogramos")]
    [ProducesResponseType(typeof(ConversionResponse), 200)]
    [ProducesResponseType(400)]
    public ActionResult<ConversionResponse> LibrasAKilogramos([FromQuery] double? valor)
    {
        if (Invalido(valor) || valor! < 0) return Error("El valor debe ser ≥ 0 (libras).");
        double res = valor.Value / 2.2046226218;
        return Ok(new ConversionResponse("libras", "kilogramos", valor.Value, res));
    }

    [HttpGet("gramos-a-onzas")]
    [ProducesResponseType(typeof(ConversionResponse), 200)]
    [ProducesResponseType(400)]
    public ActionResult<ConversionResponse> GramosAOnzas([FromQuery] double? valor)
    {
        if (Invalido(valor) || valor! < 0) return Error("El valor debe ser ≥ 0 (gramos).");
        double res = valor.Value / 28.349523125;
        return Ok(new ConversionResponse("gramos", "onzas", valor.Value, res));
    }

    [HttpGet("onzas-a-gramos")]
    [ProducesResponseType(typeof(ConversionResponse), 200)]
    [ProducesResponseType(400)]
    public ActionResult<ConversionResponse> OnzasAGramos([FromQuery] double? valor)
    {
        if (Invalido(valor) || valor! < 0) return Error("El valor debe ser ≥ 0 (onzas).");
        double res = valor.Value * 28.349523125;
        return Ok(new ConversionResponse("onzas", "gramos", valor.Value, res));
    }

    // ====================== TEMPERATURA ======================

    [HttpGet("celsius-a-fahrenheit")]
    [ProducesResponseType(typeof(ConversionResponse), 200)]
    [ProducesResponseType(400)]
    public ActionResult<ConversionResponse> CelsiusAFahrenheit([FromQuery] double? valor)
    {
        if (Invalido(valor)) return Error("Se requiere el valor (°C).");
        double res = (valor.Value * 9.0 / 5.0) + 32.0;
        return Ok(new ConversionResponse("celsius", "fahrenheit", valor.Value, res));
    }

    [HttpGet("fahrenheit-a-celsius")]
    [ProducesResponseType(typeof(ConversionResponse), 200)]
    [ProducesResponseType(400)]
    public ActionResult<ConversionResponse> FahrenheitACelsius([FromQuery] double? valor)
    {
        if (Invalido(valor)) return Error("Se requiere el valor (°F).");
        double res = (valor.Value - 32.0) * 5.0 / 9.0;
        return Ok(new ConversionResponse("fahrenheit", "celsius", valor.Value, res));
    }

    [HttpGet("celsius-a-kelvin")]
    [ProducesResponseType(typeof(ConversionResponse), 200)]
    [ProducesResponseType(400)]
    public ActionResult<ConversionResponse> CelsiusAKelvin([FromQuery] double? valor)
    {
        if (Invalido(valor)) return Error("Se requiere el valor (°C).");
        if (valor! < -273.15) return Error("Celsius no puede ser menor que -273.15 °C.");
        double res = valor.Value + 273.15;
        return Ok(new ConversionResponse("celsius", "kelvin", valor.Value, res));
    }

    [HttpGet("kelvin-a-celsius")]
    [ProducesResponseType(typeof(ConversionResponse), 200)]
    [ProducesResponseType(400)]
    public ActionResult<ConversionResponse> KelvinACelsius([FromQuery] double? valor)
    {
        if (Invalido(valor)) return Error("Se requiere el valor (K).");
        if (valor! < 0) return Error("Kelvin no puede ser negativo.");
        double res = valor.Value - 273.15;
        return Ok(new ConversionResponse("kelvin", "celsius", valor.Value, res));
    }

    [HttpGet("fahrenheit-a-kelvin")]
    [ProducesResponseType(typeof(ConversionResponse), 200)]
    [ProducesResponseType(400)]
    public ActionResult<ConversionResponse> FahrenheitAKelvin([FromQuery] double? valor)
    {
        if (Invalido(valor)) return Error("Se requiere el valor (°F).");
        if (valor! < -459.67) return Error("Fahrenheit no puede ser menor que -459.67 °F.");
        double c = (valor.Value - 32.0) * 5.0 / 9.0;
        double res = c + 273.15;
        return Ok(new ConversionResponse("fahrenheit", "kelvin", valor.Value, res));
    }

    [HttpGet("kelvin-a-fahrenheit")]
    [ProducesResponseType(typeof(ConversionResponse), 200)]
    [ProducesResponseType(400)]
    public ActionResult<ConversionResponse> KelvinAFahrenheit([FromQuery] double? valor)
    {
        if (Invalido(valor)) return Error("Se requiere el valor (K).");
        if (valor! < 0) return Error("Kelvin no puede ser negativo.");
        double c = valor.Value - 273.15;
        double res = (c * 9.0 / 5.0) + 32.0;
        return Ok(new ConversionResponse("kelvin", "fahrenheit", valor.Value, res));
    }
}
