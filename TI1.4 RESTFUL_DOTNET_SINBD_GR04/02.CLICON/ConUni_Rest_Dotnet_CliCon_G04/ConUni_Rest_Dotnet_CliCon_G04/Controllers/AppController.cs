using ConUni_Soap_Dotnet_CliCon_G04.Services;
using ConUni_Soap_Dotnet_CliCon_G04.Views;

namespace ConUni_Soap_Dotnet_CliCon_G04.Controllers;

public class AppController
{
    private readonly AuthController _auth;
    private readonly ConversorApiClient _api;
    private readonly ConversionController _conversor;

    public AppController()
    {
        _auth = new AuthController();
        _api = new ConversorApiClient();
        _conversor = new ConversionController(_api);
    }

    public async Task RunAsync()
    {
        if (!_auth.Login()) return;

        while (true)
        {
            var op = MainMenuView.PromptMain(_api.BaseUrl);
            switch (op)
            {
                case "1": await _conversor.LongitudAsync(); break;
                case "2": await _conversor.MasaAsync(); break;
                case "3": await _conversor.TemperaturaAsync(); break;
                case "4": CambiarBaseUrl(); break;
                case "0": return;
                default: ConsoleUI.Warn("Opción no válida."); ConsoleUI.Pause(); break;
            }
        }
    }

    private void CambiarBaseUrl()
    {
        Console.Write($"\nURL actual: {_api.BaseUrl}\nNueva URL (Enter para mantener): ");
        var val = Console.ReadLine()?.Trim();
        if (!string.IsNullOrWhiteSpace(val))
        {
            _api.SetBaseUrl(val);
            ConsoleUI.Success($"URL base cambiada a: {_api.BaseUrl}");
        }
        else
        {
            ConsoleUI.Warn("URL sin cambios.");
        }
        ConsoleUI.Pause();
    }
}
