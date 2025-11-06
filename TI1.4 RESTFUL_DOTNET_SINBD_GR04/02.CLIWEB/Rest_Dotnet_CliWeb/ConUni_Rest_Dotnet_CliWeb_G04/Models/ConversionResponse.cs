namespace ConUni_Rest_WebClient.Models;

public class ConversionResponse
{
    public string DeUnidad { get; set; } = "";
    public string AUnidad { get; set; } = "";
    public double ValorIngresado { get; set; }
    public double Resultado { get; set; }
}
