using System;
using System.ServiceModel;
using espe.edu.ec.monster.controlador;

namespace Host_Servidor_Dotnet_SOAP_G04
{
    internal class Program
    {
        static void Main()
        {
            using (var host = new ServiceHost(typeof(ConversionService)))
            {
                try
                {
                    host.Open();
                    Console.WriteLine("Servicio WCF iniciado.");
                    Console.WriteLine("WSDL: http://localhost:8733/Design_Time_Addresses/ConversionUnidades_SOAP/Service1/?wsdl");
                    Console.WriteLine("Presiona Enter para salir...");
                    Console.ReadLine();
                    host.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al iniciar el servicio: " + ex.Message);
                    Console.ReadLine();
                }
            }
        }
    }
}
