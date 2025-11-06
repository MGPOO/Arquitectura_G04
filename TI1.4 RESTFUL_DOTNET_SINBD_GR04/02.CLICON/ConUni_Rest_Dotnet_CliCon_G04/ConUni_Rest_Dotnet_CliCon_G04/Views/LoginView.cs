using ConUni_Soap_Dotnet_CliCon_G04.Views;

namespace ConUni_Soap_Dotnet_CliCon_G04.Views;

public static class LoginView
{
    public static (string user, string pass) Prompt()
    {
        Console.Clear();
        ConsoleUI.ShowLigaLogoLDU(); // << SOLO aquí mostramos el logo
        ConsoleUI.Header("Conversor Rest Dotnet G04");
        ConsoleUI.Subtitle("Iniciar Sesión");
        Console.Write("Usuario: ");
        var u = Console.ReadLine()?.Trim() ?? "";
        Console.Write("Contraseña: ");
        var p = ConsoleUI.ReadPassword();
        return (u, p);
    }
}
