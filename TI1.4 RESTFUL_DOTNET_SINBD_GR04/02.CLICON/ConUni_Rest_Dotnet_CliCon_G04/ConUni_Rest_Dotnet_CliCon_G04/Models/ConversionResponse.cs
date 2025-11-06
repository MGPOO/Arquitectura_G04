namespace ConUni_Soap_Dotnet_CliCon_G04.Models;

public sealed record ConversionResponse(
    string DeUnidad,
    string AUnidad,
    double ValorIngresado,
    double Resultado
);
