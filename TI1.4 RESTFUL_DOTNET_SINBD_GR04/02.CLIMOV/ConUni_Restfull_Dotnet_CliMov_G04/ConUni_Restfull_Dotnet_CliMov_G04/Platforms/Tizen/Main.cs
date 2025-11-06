using System;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace ConUni_Soap_Dotnet_CliMov_G04
{
    internal class Program : MauiApplication
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }
    }
}
