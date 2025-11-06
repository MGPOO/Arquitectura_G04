namespace Cliente_Rest_G04.Models;

public sealed class ConversionResponse
{
    public string DeUnidad { get; set; } = "";
    public string AUnidad { get; set; } = "";
    public double ValorIngresado { get; set; }
    public double Resultado { get; set; }
}
