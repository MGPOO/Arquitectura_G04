using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using ClienteRestWpf.Models;

namespace ClienteRestWpf
{
    public class ApiConversorClient
    {
        // Cambia esta URL a la de tu servidor
        private const string ApiBase = "https://localhost:7090/api/conversor/";
        private readonly HttpClient _http = new HttpClient { BaseAddress = new Uri(ApiBase) };

        public async Task<ConversionResponse?> GetAsync(string endpoint, double valor, CancellationToken ct = default)
        {
            var url = $"{endpoint}?valor={valor.ToString(System.Globalization.CultureInfo.InvariantCulture)}";
            var res = await _http.GetAsync(url, ct);
            res.EnsureSuccessStatusCode();
            return await res.Content.ReadFromJsonAsync<ConversionResponse>(cancellationToken: ct);
        }
    }
}
