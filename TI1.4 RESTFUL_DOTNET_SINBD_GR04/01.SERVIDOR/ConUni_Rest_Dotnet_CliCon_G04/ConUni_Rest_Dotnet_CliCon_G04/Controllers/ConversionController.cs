using ConUni_Soap_Dotnet_CliCon_G04.Services;
using ConUni_Soap_Dotnet_CliCon_G04.Views;

namespace ConUni_Soap_Dotnet_CliCon_G04.Controllers;

public class ConversionController
{
    private readonly ConversorApiClient _api;

    public ConversionController(ConversorApiClient api) => _api = api;

    // -------- LONGITUD (no negativos) --------
    public async Task LongitudAsync()
    {
        while (true)
        {
            var op = Views.MainMenuView.PromptLongitud();

            (string? endpoint, string? label) = op switch
            {
                "1" => ("centimetros-a-pies", "Centímetros -> Pies"),
                "2" => ("pies-a-centimetros", "Pies -> Centímetros"),
                "3" => ("pulgadas-a-centimetros", "Pulgadas -> Centímetros"),
                "4" => ("centimetros-a-pulgadas", "Centímetros -> Pulgadas"),
                "5" => ("metros-a-yardas", "Metros -> Yardas"),
                "6" => ("yardas-a-metros", "Yardas -> Metros"),
                "0" => (null, null),
                _ => ("__bad__", null)
            };

            if (endpoint is null) return;
            if (endpoint == "__bad__") { ConsoleUI.Warn("Opción no válida."); ConsoleUI.Pause(); continue; }

            ConsoleUI.Success($"\nElegiste: {label}");
            var valor = ConsoleUI.ReadDoubleStrict("\nIngresa el valor a convertir: ", allowNegative: false);

            var (ok, data, error) = await _api.ConvertAsync(endpoint!, valor);
            if (ok && data is not null) ConversionView.ShowResult(data);
            else ConsoleUI.Error($"\nError: {error}");
            ConsoleUI.Pause();
        }
    }

    // -------- MASA (no negativos) --------
    public async Task MasaAsync()
    {
        while (true)
        {
            var op = Views.MainMenuView.PromptMasa();

            (string? endpoint, string? label) = op switch
            {
                "1" => ("kilogramos-a-libras", "Kilogramos -> Libras"),
                "2" => ("libras-a-kilogramos", "Libras -> Kilogramos"),
                "3" => ("gramos-a-onzas", "Gramos -> Onzas"),
                "4" => ("onzas-a-gramos", "Onzas -> Gramos"),
                "0" => (null, null),
                _ => ("__bad__", null)
            };

            if (endpoint is null) return;
            if (endpoint == "__bad__") { ConsoleUI.Warn("Opción no válida."); ConsoleUI.Pause(); continue; }

            ConsoleUI.Success($"\nElegiste: {label}");
            var valor = ConsoleUI.ReadDoubleStrict("\nIngresa el valor a convertir: ", allowNegative: false);

            var (ok, data, error) = await _api.ConvertAsync(endpoint!, valor);
            if (ok && data is not null) ConversionView.ShowResult(data);
            else ConsoleUI.Error($"\nError: {error}");
            ConsoleUI.Pause();
        }
    }

    // -------- TEMPERATURA (sí permite negativos) --------
    public async Task TemperaturaAsync()
    {
        while (true)
        {
            var op = Views.MainMenuView.PromptTemperatura();

            (string? endpoint, string? label) = op switch
            {
                "1" => ("celsius-a-fahrenheit", "Celsius -> Fahrenheit"),
                "2" => ("fahrenheit-a-celsius", "Fahrenheit -> Celsius"),
                "3" => ("celsius-a-kelvin", "Celsius -> Kelvin"),
                "4" => ("kelvin-a-celsius", "Kelvin -> Celsius"),
                "5" => ("fahrenheit-a-kelvin", "Fahrenheit -> Kelvin"),
                "6" => ("kelvin-a-fahrenheit", "Kelvin -> Fahrenheit"),
                "0" => (null, null),
                _ => ("__bad__", null)
            };

            if (endpoint is null) return;
            if (endpoint == "__bad__") { ConsoleUI.Warn("Opción no válida."); ConsoleUI.Pause(); continue; }

            ConsoleUI.Success($"\nElegiste: {label}");
            var valor = ConsoleUI.ReadDoubleStrict("\nIngresa el valor a convertir: ", allowNegative: true);

            var (ok, data, error) = await _api.ConvertAsync(endpoint!, valor);
            if (ok && data is not null) ConversionView.ShowResult(data);
            else ConsoleUI.Error($"\nError: {error}");
            ConsoleUI.Pause();
        }
    }
}
