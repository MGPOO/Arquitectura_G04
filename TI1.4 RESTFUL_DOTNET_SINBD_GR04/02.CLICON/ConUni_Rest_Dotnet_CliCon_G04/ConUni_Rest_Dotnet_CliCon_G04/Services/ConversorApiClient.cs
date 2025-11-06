using System.Net.Http.Json;
using System.Text.Json;
using ConUni_Soap_Dotnet_CliCon_G04.Models;

namespace ConUni_Soap_Dotnet_CliCon_G04.Services;

public class ConversorApiClient
{
    public string BaseUrl { get; private set; } = "https://localhost:7090"; // ajusta a tu puerto

    private readonly HttpClient _http;
    private static readonly JsonSerializerOptions _json = new() { PropertyNameCaseInsensitive = true };

    public ConversorApiClient()
    {
        _http = new HttpClient(new HttpClientHandler
        {
            // Para certificados de desarrollo en localhost
            ServerCertificateCustomValidationCallback = (msg, cert, chain, err) => true
        })
        { Timeout = TimeSpan.FromSeconds(10) };
    }

    public void SetBaseUrl(string url)
    {
        if (!string.IsNullOrWhiteSpace(url))
            BaseUrl = url.Trim().TrimEnd('/');
    }

    public async Task<(bool ok, ConversionResponse? data, string? error)> ConvertAsync(string endpoint, double valor)
    {
        try
        {
            string url = $"{BaseUrl}/api/conversor/{endpoint}?valor={valor.ToString(System.Globalization.CultureInfo.InvariantCulture)}";
            var res = await _http.GetAsync(url);

            if (res.IsSuccessStatusCode)
            {
                var data = await res.Content.ReadFromJsonAsync<ConversionResponse>(_json);
                return (true, data, null);
            }
            else
            {
                var text = await res.Content.ReadAsStringAsync();
                return (false, null, $"HTTP {(int)res.StatusCode}: {res.ReasonPhrase}\n{text}");
            }
        }
        catch (Exception ex)
        {
            return (false, null, ex.Message);
        }
    }
}
