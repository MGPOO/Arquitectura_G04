using ConversorRef;

namespace ConUni_Soap_Dotnet_CliEsc_G04.Services
{
    public static class SoapClientFactory
    {
        public static ConversionServiceClient Create()
        {
            // Si tu Connected Service generó otro nombre de clase, ajusta aquí:
            return new ConversionServiceClient();
        }
    }
}
