using System.Windows;
using ConUni_Soap_Dotnet_CliEsc_G04.Controllers;
using ConUni_Soap_Dotnet_CliEsc_G04.Views;

namespace ConUni_Soap_Dotnet_CliEsc_G04
{
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var account = new Controllers.AccountController();

            var login = new Views.LoginView(account);
            var ok = login.ShowDialog() == true;

            if (!ok)
            {
                Shutdown();
                return;
            }

            var main = new Views.MainView(account);
            MainWindow = main;     // <- importante
            main.Show();

            // si quieres, puedes volver al modo normal:
            // ShutdownMode = ShutdownMode.OnLastWindowClose;
        }

    }
}
