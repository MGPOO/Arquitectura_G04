using System;
using ClienteConversionConsoleMvc.Models;
using ClienteConversionConsoleMvc.Views;

namespace ClienteConversionConsoleMvc.Controllers
{
    public class ConversionController
    {
        private readonly ConversionModel _model;
        private readonly ConsoleView _view;

        public ConversionController(ConversionModel model, ConsoleView view)
        {
            _model = model;
            _view = view;
        }

        public bool Authenticate(string user, string pass)
        {
            const int maxAttempts = 3;

            for (int i = 1; i <= maxAttempts; i++)
            {
                Console.Clear();
                _view.ShowLigaLogoLDU(60);
                _view.Write("-------------------------------------------\n");
                _view.WriteInfo("Autenticación - Cliente ConversionService");
                _view.Write("-------------------------------------------\n");

                _view.Write("Usuario: ");
                string u = Console.ReadLine() ?? "";

                _view.Write("Contraseña: ");
                string p = _view.ReadPassword();

                if (u == user && p == pass)
                {
                    _view.WriteOk("\nAutenticación correcta. Bienvenido.");
                    _view.PauseShort();
                    return true;
                }

                _view.WriteWarn("\nUsuario o contraseña incorrectos.");
                if (i < maxAttempts)
                {
                    _view.WriteInfo($"Intentos restantes: {maxAttempts - i}");
                    _view.PauseShort();
                }
            }

            _view.WriteWarn("\nSe superaron los intentos permitidos. Saliendo...");
            return false;
        }

        public void MainMenu()
        {
            while (true)
            {
                _view.DrawHeader("Cliente MVC - Conversiones");
                _view.DrawMenu(new[]
                {
                    "Longitud (cm, ft, m, yd, in)",
                    "Masa (kg, lb, g, oz)",
                    "Temperatura (°C, °F, K)",
                    "Pruebas rápidas (demo)",
                    "Salir"
                });

                int op = _view.ReadOption(1, 5);
                if (op == 5) break;

                try
                {
                    switch (op)
                    {
                        case 1: MenuLength(); break;
                        case 2: MenuMass(); break;
                        case 3: MenuTemp(); break;
                        case 4: DemoQuick(); break;
                    }
                }
                catch (Exception ex) { _view.WriteError(ex); }

                _view.Pause();
            }
        }

        // ===== Submenús =====
        void MenuLength()
        {
            _view.DrawHeader("Longitud");
            _view.DrawMenu(new[]
            {
                "Centímetros → Pies (cm → ft)",
                "Pies → Centímetros (ft → cm)",
                "Metros → Yardas (m → yd)",
                "Yardas → Metros (yd → m)",
                "Pulgadas → Centímetros (in → cm)",
                "Centímetros → Pulgadas (cm → in)",
                "Volver"
            });

            int op = _view.ReadOption(1, 7);
            if (op == 7) return;

            double v = _view.ReadDouble("Ingrese el valor:");
            double r = 0;

            switch (op)
            {
                case 1: r = _model.CentimetersToFeet(v); break;
                case 2: r = _model.FeetToCentimeters(v); break;
                case 3: r = _model.MetersToYards(v); break;
                case 4: r = _model.YardsToMeters(v); break;
                case 5: r = _model.InchesToCentimeters(v); break;
                case 6: r = _model.CentimetersToInches(v); break;
            }

            _view.WriteOk("\nResultado: " + r);
        }

        void MenuMass()
        {
            _view.DrawHeader("Masa");
            _view.DrawMenu(new[]
            {
                "Kilogramos → Libras (kg → lb)",
                "Libras → Kilogramos (lb → kg)",
                "Gramos → Onzas (g → oz)",
                "Onzas → Gramos (oz → g)",
                "Volver"
            });

            int op = _view.ReadOption(1, 5);
            if (op == 5) return;

            double v = _view.ReadDouble("Ingrese el valor:");
            double r = 0;

            switch (op)
            {
                case 1: r = _model.KilogramsToPounds(v); break;
                case 2: r = _model.PoundsToKilograms(v); break;
                case 3: r = _model.GramsToOunces(v); break;
                case 4: r = _model.OuncesToGrams(v); break;
            }

            _view.WriteOk("\nResultado: " + r);
        }

        void MenuTemp()
        {
            _view.DrawHeader("Temperatura");
            _view.DrawMenu(new[]
            {
                "Celsius → Fahrenheit (°C → °F)",
                "Fahrenheit → Celsius (°F → °C)",
                "Celsius → Kelvin (°C → K)",
                "Kelvin → Celsius (K → °C)",
                "Fahrenheit → Kelvin (°F → K)",
                "Kelvin → Fahrenheit (K → °F)",
                "Volver"
            });

            int op = _view.ReadOption(1, 7);
            if (op == 7) return;

            double v = _view.ReadDouble("Ingrese el valor:");
            double r = 0;

            switch (op)
            {
                case 1: r = _model.CelsiusToFahrenheit(v); break;
                case 2: r = _model.FahrenheitToCelsius(v); break;
                case 3: r = _model.CelsiusToKelvin(v); break;
                case 4: r = _model.KelvinToCelsius(v); break;
                case 5: r = _model.FahrenheitToKelvin(v); break;
                case 6: r = _model.KelvinToFahrenheit(v); break;
            }

            _view.WriteOk("\nResultado: " + r);
        }

        void DemoQuick()
        {
            _view.WriteInfo("Resultados demo:");
            _view.WriteOk("25°C → " + _model.CelsiusToKelvin(25) + " K");
            _view.WriteOk("0°C  → " + _model.CelsiusToFahrenheit(0) + " °F");
            _view.WriteOk("10 m → " + _model.MetersToYards(10) + " yd");
            _view.WriteOk("5 kg → " + _model.KilogramsToPounds(5) + " lb");
        }
    }
}
