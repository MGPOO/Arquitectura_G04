using System.Threading.Tasks;
using ConversorRef;

namespace ConUni_Soap_Dotnet_CliWeb_G04.Services
{
    public class ConversorSoapClient
    {
        // ← AJUSTA a tu endpoint real (sin ?wsdl)
        private const string ServiceBaseAddress = "http://localhost:52091/ec.edu.espe.monster.ws/ConversionService.svc";

        private static ConversionServiceClient CreateClient() =>
            new ConversionServiceClient(
                ConversionServiceClient.EndpointConfiguration.BasicHttpBinding_IConversionService,
                ServiceBaseAddress
            );

        // ---------- LONGITUD ----------
        public async Task<double> CentimetrosAPiesAsync(double cm)
        {
            var c = CreateClient(); var r = await c.CentimetersToFeetAsync(cm); await c.CloseAsync(); return r;
        }
        public async Task<double> PiesACentimetrosAsync(double ft)
        {
            var c = CreateClient(); var r = await c.FeetToCentimetersAsync(ft); await c.CloseAsync(); return r;
        }
        public async Task<double> MetrosAYardasAsync(double m)
        {
            var c = CreateClient(); var r = await c.MetersToYardsAsync(m); await c.CloseAsync(); return r;
        }
        public async Task<double> YardasAMetrosAsync(double yd)
        {
            var c = CreateClient(); var r = await c.YardsToMetersAsync(yd); await c.CloseAsync(); return r;
        }
        public async Task<double> PulgadasACentimetrosAsync(double inches)
        {
            var c = CreateClient(); var r = await c.InchesToCentimetersAsync(inches); await c.CloseAsync(); return r;
        }
        public async Task<double> CentimetrosAPulgadasAsync(double cm)
        {
            var c = CreateClient(); var r = await c.CentimetersToInchesAsync(cm); await c.CloseAsync(); return r;
        }

        // ---------- MASA ----------
        public async Task<double> KgALibrasAsync(double kg)
        {
            var c = CreateClient(); var r = await c.KilogramsToPoundsAsync(kg); await c.CloseAsync(); return r;
        }
        public async Task<double> LibrasAKgAsync(double lb)
        {
            var c = CreateClient(); var r = await c.PoundsToKilogramsAsync(lb); await c.CloseAsync(); return r;
        }
        public async Task<double> GramosAOnzasAsync(double g)
        {
            var c = CreateClient(); var r = await c.GramsToOuncesAsync(g); await c.CloseAsync(); return r;
        }
        public async Task<double> OnzasAGramosAsync(double oz)
        {
            var c = CreateClient(); var r = await c.OuncesToGramsAsync(oz); await c.CloseAsync(); return r;
        }

        // ---------- TEMPERATURA ----------
        public async Task<double> CelsiusAFahrenheitAsync(double celsius)
        {
            var c = CreateClient(); var r = await c.CelsiusToFahrenheitAsync(celsius); await c.CloseAsync(); return r;
        }
        public async Task<double> FahrenheitACelsiusAsync(double f)
        {
            var c = CreateClient(); var r = await c.FahrenheitToCelsiusAsync(f); await c.CloseAsync(); return r;
        }
        public async Task<double> CelsiusAKelvinAsync(double celsius)
        {
            var c = CreateClient(); var r = await c.CelsiusToKelvinAsync(celsius); await c.CloseAsync(); return r;
        }
        public async Task<double> KelvinACelsiusAsync(double k)
        {
            var c = CreateClient(); var r = await c.KelvinToCelsiusAsync(k); await c.CloseAsync(); return r;
        }
        public async Task<double> FahrenheitAKelvinAsync(double f)
        {
            var c = CreateClient(); var r = await c.FahrenheitToKelvinAsync(f); await c.CloseAsync(); return r;
        }
        public async Task<double> KelvinAFahrenheitAsync(double k)
        {
            var c = CreateClient(); var r = await c.KelvinToFahrenheitAsync(k); await c.CloseAsync(); return r;
        }
    }
}
