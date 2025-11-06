using ConUni_Soap_Dotnet_CliCon_G04.Views;

namespace ConUni_Soap_Dotnet_CliCon_G04.Controllers;

public class AuthController
{
    // Validación ESTRICTA y SENSIBLE A MAYÚSCULAS
    private const string USER = "MONSTER";
    private const string PASS = "monster9";

    public bool Login()
    {
        for (int i = 1; i <= 3; i++)
        {
            var (u, p) = LoginView.Prompt();

            // Usuario debe coincidir EXACTAMENTE (Ordinal)
            if (string.Equals(u, USER, StringComparison.Ordinal) && p == PASS)
            {
                ConsoleUI.Success("\nInicio de sesión exitoso. ¡Bienvenido, MONSTER!");
                Thread.Sleep(900);
                return true;
            }

            ConsoleUI.Warn($"\nCredenciales incorrectas (intento {i}/3).");
            Thread.Sleep(2000);
        }

        ConsoleUI.Error("\nDemasiados intentos fallidos.");
        return false;
    }
}
