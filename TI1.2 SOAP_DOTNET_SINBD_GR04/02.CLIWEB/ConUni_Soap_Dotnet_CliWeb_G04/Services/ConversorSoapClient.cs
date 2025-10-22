// Services/ConversorSoapClient.cs
using System.Threading.Tasks;
using ConversorRef; // <- del Reference.cs

namespace ConUni_Soap_Dotnet_CliWeb_G04.Services
{
    public class ConversorSoapClient
    {
        // TODO: pon aquí tu URL REAL del servicio (SIN ?wsdl y con / final si tu servicio lo usa)
        private const string ServiceBaseAddress = "http://localhost:8733/Design_Time_Addresses/ConversionUnidades_SOAP/Service1";

        private static ConversionServiceClient CreateClient()
        {
            return new ConversionServiceClient(
                ConversionServiceClient.EndpointConfiguration.BasicHttpBinding_IConversionService,
                ServiceBaseAddress
            );
        }

        // --------- MAPEOS “amigables” -> métodos reales del servicio ---------

        // Kg -> Libras  (usa KilogramsToPoundsAsync del servicio)
        public async Task<double> KgALibrasAsync(double kg)
        {
            var client = CreateClient();
            var result = await client.KilogramsToPoundsAsync(kg);
            await client.CloseAsync();
            return result;
        }

        // Libras -> Kg  (por si lo necesitas)
        public async Task<double> LibrasAKgAsync(double lb)
        {
            var client = CreateClient();
            var result = await client.PoundsToKilogramsAsync(lb);
            await client.CloseAsync();
            return result;
        }

        // Metros -> Yardas (tu “MetrosAPies” original no existe en el contrato; el servicio expone MetersToYards)
        public async Task<double> MetrosAYardasAsync(double metros)
        {
            var client = CreateClient();
            var result = await client.MetersToYardsAsync(metros);
            await client.CloseAsync();
            return result;
        }

        // Centímetros -> Pies (si quieres pies)
        public async Task<double> CentimetrosAPiesAsync(double cm)
        {
            var client = CreateClient();
            var result = await client.CentimetersToFeetAsync(cm);
            await client.CloseAsync();
            return result;
        }

        // También puedes exponer otros atajos del contrato si los vas a usar:
        public async Task<double> CentimetrosAPulgadasAsync(double cm)
        {
            var client = CreateClient();
            var result = await client.CentimetersToInchesAsync(cm);
            await client.CloseAsync();
            return result;
        }

        public async Task<double> PulgadasACentimetrosAsync(double inches)
        {
            var client = CreateClient();
            var result = await client.InchesToCentimetersAsync(inches);
            await client.CloseAsync();
            return result;
        }

        public async Task<double> CelsiusAFahrenheitAsync(double c)
        {
            var client = CreateClient();
            var result = await client.CelsiusToFahrenheitAsync(c);
            await client.CloseAsync();
            return result;
        }

        // ...y así con los demás métodos del contrato cuando los necesites.
    }
}
