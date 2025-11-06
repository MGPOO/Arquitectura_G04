using ConUni_Soap_Dotnet_CliCon_G04.Models;

namespace ConUni_Soap_Dotnet_CliCon_G04.Views;

public static class ConversionView
{
    // Usa la validación estricta (punto decimal, NaN/Inf, signo opcional)
    public static double AskValor(bool allowNegative)
        => ConsoleUI.ReadDoubleStrict("\nIngresa el valor a convertir: ", allowNegative);

    public static void ShowResult(ConversionResponse data)
    {
        ConsoleUI.Success("\nConversión exitosa:");
        Console.WriteLine($"  De:       {data.DeUnidad}");
        Console.WriteLine($"  A:        {data.AUnidad}");
        Console.WriteLine($"  Entrada:  {data.ValorIngresado}");
        Console.WriteLine($"  Resultado:{data.Resultado}");
    }
}
