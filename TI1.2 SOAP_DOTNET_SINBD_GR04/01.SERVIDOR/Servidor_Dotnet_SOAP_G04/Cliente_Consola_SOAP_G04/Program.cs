using System;
using Cliente_Consola_SOAP_G04.ConversionService;

namespace Cliente_Consola_SOAP_G04
{
    internal class Program
    {
        static void Main()
        {
            var client = new ConversionServiceClient();

            Console.WriteLine("10 cm -> in: " + client.CentimetersToInches(10));
            Console.WriteLine("1 m  -> yd: " + client.MetersToYards(1));
            Console.WriteLine("70 kg -> lb: " + client.KilogramsToPounds(70));
            Console.WriteLine("25 °C -> °F: " + client.CelsiusToFahrenheit(25));

            client.Close();
            Console.WriteLine("Listo. Presiona Enter para salir.");
            Console.ReadLine();
        }
    }
}
