using System;
using ClienteConversionConsoleMvc.Controllers;
using ClienteConversionConsoleMvc.Models;
using ClienteConversionConsoleMvc.Views;

namespace ClienteConversionConsoleMvc
{
    class Program
    {
        const string LOGIN_USER = "MONSTER";
        const string LOGIN_PASS = "monster9";

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Cliente MVC - Conversiones";

            var model = new ConversionModel();
            var view = new ConsoleView();
            var controller = new ConversionController(model, view);

            if (!controller.Authenticate(LOGIN_USER, LOGIN_PASS))
                return;

            controller.MainMenu();
            view.WriteInfo("\nGracias por usar el cliente. Presiona una tecla para cerrar…");
            Console.ReadKey(true);
        }
    }
}
