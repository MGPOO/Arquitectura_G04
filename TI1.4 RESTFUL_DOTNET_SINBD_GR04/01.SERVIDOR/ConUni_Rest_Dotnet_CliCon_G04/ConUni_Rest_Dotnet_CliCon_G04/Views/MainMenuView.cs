namespace ConUni_Soap_Dotnet_CliCon_G04.Views;

public static class MainMenuView
{
    public static string PromptMain(string baseUrl)
    {
        Console.Clear();
        ConsoleUI.Header("Conversor de Unidades (Cliente Consola Rest)");
        Console.WriteLine("  1) Longitud");
        Console.WriteLine("  2) Masa");
        Console.WriteLine("  3) Temperatura");
        Console.WriteLine("  0) Salir");
        Console.Write("\nOpción: ");
        return Console.ReadLine()?.Trim() ?? "";
    }

    public static string PromptLongitud()
    {
        Console.Clear();
        ConsoleUI.Header("Longitud");
        Console.WriteLine("  1) Centímetros -> Pies");
        Console.WriteLine("  2) Pies -> Centímetros");
        Console.WriteLine("  3) Pulgadas -> Centímetros");
        Console.WriteLine("  4) Centímetros -> Pulgadas");
        Console.WriteLine("  5) Metros -> Yardas");
        Console.WriteLine("  6) Yardas -> Metros");
        Console.WriteLine("  0) Volver");
        Console.Write("\nOpción: ");
        return Console.ReadLine()?.Trim() ?? "";
    }

    public static string PromptMasa()
    {
        Console.Clear();
        ConsoleUI.Header("Masa");
        Console.WriteLine("  1) Kilogramos -> Libras");
        Console.WriteLine("  2) Libras -> Kilogramos");
        Console.WriteLine("  3) Gramos -> Onzas");
        Console.WriteLine("  4) Onzas -> Gramos");
        Console.WriteLine("  0) Volver");
        Console.Write("\nOpción: ");
        return Console.ReadLine()?.Trim() ?? "";
    }

    public static string PromptTemperatura()
    {
        Console.Clear();
        ConsoleUI.Header("Temperatura");
        Console.WriteLine("  1) Celsius -> Fahrenheit");
        Console.WriteLine("  2) Fahrenheit -> Celsius");
        Console.WriteLine("  3) Celsius -> Kelvin");
        Console.WriteLine("  4) Kelvin -> Celsius");
        Console.WriteLine("  5) Fahrenheit -> Kelvin");
        Console.WriteLine("  6) Kelvin -> Fahrenheit");
        Console.WriteLine("  0) Volver");
        Console.Write("\nOpción: ");
        return Console.ReadLine()?.Trim() ?? "";
    }
}
