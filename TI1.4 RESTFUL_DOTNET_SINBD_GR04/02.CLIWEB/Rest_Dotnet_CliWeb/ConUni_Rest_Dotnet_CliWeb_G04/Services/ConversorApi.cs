using System.Globalization;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace ConUni_Rest_Dotnet_CliWeb_G04.Services
{
    public sealed class ConversionResponse
    {
        public string DeUnidad { get; set; } = "";
        public string AUnidad { get; set; } = "";
        public double ValorIngresado { get; set; }
        public double Resultado { get; set; }
    }

    public class ConversorApi
    {
        private readonly HttpClient _http;
        private readonly ILogger<ConversorApi> _logger;

        public ConversorApi(HttpClient http, ILogger<ConversorApi> logger)
        {
            _http = http;
            _logger = logger;
        }

        public async Task<(bool ok, ConversionResponse? data, string? error)>
            ConvertAsync(string endpoint, double valor)
        {
            try
            {
                var url = $"api/conversion/{endpoint}?valor={valor.ToString(CultureInfo.InvariantCulture)}";

                using var resp = await _http.GetAsync(url);

                if (!resp.IsSuccessStatusCode)
                {
                    var body = await resp.Content.ReadAsStringAsync();
                    return (false, null, $"{(int)resp.StatusCode} {resp.ReasonPhrase}: {body}");
                }

                var json = await resp.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<ConversionResponse>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return (true, data, null);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error HTTP hacia {Base}/{Endpoint}", _http.BaseAddress, endpoint);
                var inner = ex.InnerException?.Message is string m ? $" — {m}" : "";
                return (false, null, $"{ex.Message}{inner}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado hacia {Base}/{Endpoint}", _http.BaseAddress, endpoint);
                return (false, null, ex.Message);
            }
        }
    }
}
