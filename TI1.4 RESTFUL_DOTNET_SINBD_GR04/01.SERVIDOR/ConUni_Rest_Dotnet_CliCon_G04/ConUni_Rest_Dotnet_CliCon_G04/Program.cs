using ConUni_Soap_Dotnet_CliCon_G04.Controllers;

namespace ConUni_Soap_Dotnet_CliCon_G04;

public class Program
{
    public static async Task Main()
    {
        Console.Title = "Conversor REST – Cliente Consola MVC (G04)";
        var app = new AppController();
        await app.RunAsync();
    }
}
