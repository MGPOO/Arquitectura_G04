using System;
using System.Globalization;
using System.Text;
using ClienteConversionConsole.ConversionRef; // <— tu Service Reference

namespace ClienteConversionConsole
{
    class Program
    {
        // Paleta de colores
        static readonly ConsoleColor ColHeader = ConsoleColor.Blue;
        static readonly ConsoleColor ColMenu = ConsoleColor.Green;  
        static readonly ConsoleColor ColInfo = ConsoleColor.White;
        static readonly ConsoleColor ColOk = ConsoleColor.Green;
        static readonly ConsoleColor ColErr = ConsoleColor.Blue;

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8; // bordes Unicode
            Console.Title = "Cliente SOAP - ConversionService";

            using (var client = new ConversionServiceClient())
            {
                while (true)
                {
                    DrawHeader("Cliente SOAP - Conversiones");
                    WriteLineC("Seleccione un módulo:", ColInfo);
                    DrawMenu(new[]
                    {
                        "Longitud (cm, ft, m, yd, in)",
                        "Masa (kg, lb, g, oz)",
                        "Temperatura (°C, °F, K)",
                        "Pruebas rápidas (demo)",
                        "Salir"
                    });

                    int op = ReadOption(1, 5);
                    if (op == 5) break;

                    switch (op)
                    {
                        case 1: MenuLength(client); break;
                        case 2: MenuMass(client); break;
                        case 3: MenuTemp(client); break;
                        case 4: DemoQuick(client); break;
                    }
                }
            }

            WriteLineC("\nGracias por usar el cliente. Presiona una tecla para cerrar…", ColInfo);
            Console.ReadKey(true);
        }

        // ======= MENÚS =======

        static void MenuLength(ConversionServiceClient client)
        {
            DrawHeader("Longitud");
            DrawMenu(new[]
            {
                "Centímetros → Pies (cm → ft)",
                "Pies → Centímetros (ft → cm)",
                "Metros → Yardas (m → yd)",
                "Yardas → Metros (yd → m)",
                "Pulgadas → Centímetros (in → cm)",
                "Centímetros → Pulgadas (cm → in)",
                "Volver"
            });

            int op = ReadOption(1, 7);
            if (op == 7) return;

            double val = ReadDouble("Ingrese el valor:");
            double r = 0;

            try
            {
                switch (op)
                {
                    case 1: r = client.CentimetersToFeet(val); break;
                    case 2: r = client.FeetToCentimeters(val); break;
                    case 3: r = client.MetersToYards(val); break;
                    case 4: r = client.YardsToMeters(val); break;
                    case 5: r = client.InchesToCentimeters(val); break;
                    case 6: r = client.CentimetersToInches(val); break;
                }

                WriteResult(r);
            }
            catch (Exception ex)
            {
                WriteError(ex);
            }

            Pause();
        }

        static void MenuMass(ConversionServiceClient client)
        {
            DrawHeader("Masa");
            DrawMenu(new[]
            {
                "Kilogramos → Libras (kg → lb)",
                "Libras → Kilogramos (lb → kg)",
                "Gramos → Onzas (g → oz)",
                "Onzas → Gramos (oz → g)",
                "Volver"
            });

            int op = ReadOption(1, 5);
            if (op == 5) return;

            double val = ReadDouble("Ingrese el valor:");
            double r = 0;

            try
            {
                switch (op)
                {
                    case 1: r = client.KilogramsToPounds(val); break;
                    case 2: r = client.PoundsToKilograms(val); break;
                    case 3: r = client.GramsToOunces(val); break;
                    case 4: r = client.OuncesToGrams(val); break;
                }

                WriteResult(r);
            }
            catch (Exception ex)
            {
                WriteError(ex);
            }

            Pause();
        }

        static void MenuTemp(ConversionServiceClient client)
        {
            DrawHeader("Temperatura");
            DrawMenu(new[]
            {
                "Celsius → Fahrenheit (°C → °F)",
                "Fahrenheit → Celsius (°F → °C)",
                "Celsius → Kelvin (°C → K)",
                "Kelvin → Celsius (K → °C)",
                "Fahrenheit → Kelvin (°F → K)",
                "Kelvin → Fahrenheit (K → °F)",
                "Volver"
            });

            int op = ReadOption(1, 7);
            if (op == 7) return;

            double val = ReadDouble("Ingrese el valor:");
            double r = 0;

            try
            {
                switch (op)
                {
                    case 1: r = client.CelsiusToFahrenheit(val); break;
                    case 2: r = client.FahrenheitToCelsius(val); break;
                    case 3: r = client.CelsiusToKelvin(val); break;
                    case 4: r = client.KelvinToCelsius(val); break;
                    case 5: r = client.FahrenheitToKelvin(val); break;
                    case 6: r = client.KelvinToFahrenheit(val); break;
                }

                WriteResult(r);
            }
            catch (Exception ex)
            {
                WriteError(ex);
            }

            Pause();
        }

        static void DemoQuick(ConversionServiceClient client)
        {
            DrawHeader("Pruebas rápidas");
            try
            {
                double k = client.CelsiusToKelvin(25);
                double f = client.CelsiusToFahrenheit(0);
                double yd = client.MetersToYards(10);
                double lb = client.KilogramsToPounds(5);

                WriteLineC("Resultados demo:", ColInfo);
                WriteLineC($"25°C → {k} K", ColOk);
                WriteLineC($"0°C → {f} °F", ColOk);
                WriteLineC($"10 m → {yd} yd", ColOk);
                WriteLineC($"5 kg → {lb} lb", ColOk);
            }
            catch (Exception ex)
            {
                WriteError(ex);
            }

            Pause();
        }

        // ======= UI Helpers =======

        static void DrawHeader(string title)
        {
            Console.Clear();
            int w = Math.Max(60, title.Length + 6);
            string top = "┌" + new string('─', w - 2) + "┐";
            string mid = "│ " + PadCenter(title, w - 4) + " │";
            string bot = "└" + new string('─', w - 2) + "┘";

            WriteLineC(top, ColHeader);
            WriteLineC(mid, ColHeader);
            WriteLineC(bot, ColHeader);
        }

        static void DrawMenu(string[] items)
        {
            for (int i = 0; i < items.Length; i++)
                WriteLineC($"  {i + 1}. {items[i]}", ColMenu);

            Console.WriteLine();
        }

        static int ReadOption(int min, int max)
        {
            while (true)
            {
                WriteC($"Elige una opción [{min}-{max}]: ", ColInfo);
                string s = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(s))
                {
                    WriteLineC("Entrada vacía. Intenta de nuevo.", ColErr);
                    continue;
                }

                int op;
                if (int.TryParse(s, out op) && op >= min && op <= max)
                    return op;

                WriteLineC("Opción inválida. Intenta de nuevo.", ColErr);
            }
        }

        static double ReadDouble(string prompt)
        {
            while (true)
            {
                WriteC(prompt + " ", ColInfo);
                string s = Console.ReadLine() ?? string.Empty;

                s = s.Trim().Replace(',', '.'); // acepta coma o punto

                double val;
                if (double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out val))
                    return val;

                WriteLineC("Número inválido. Ejemplos válidos: 12,5 | 12.5 | -3 | 0.75", ColErr);
            }
        }

        static void WriteResult(double value)
        {
            WriteLineC("\nResultado: " + value, ColOk);
        }

        static void WriteError(Exception ex)
        {
            var prev = Console.ForegroundColor;
            Console.ForegroundColor = ColErr;
            Console.WriteLine("\n⚠ Error: " + ex.Message);
            if (ex.InnerException != null)
                Console.WriteLine("   Detalle: " + ex.InnerException.Message);
            Console.ForegroundColor = prev;
        }

        static void Pause()
        {
            WriteLineC("\nPresiona una tecla para continuar…", ColInfo);
            Console.ReadKey(true);
        }

        static void WriteLineC(string text, ConsoleColor color)
        {
            var prev = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = prev;
        }

        static void WriteC(string text, ConsoleColor color)
        {
            var prev = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = prev;
        }

        static string PadCenter(string text, int totalWidth)
        {
            if (text.Length >= totalWidth) return text.Substring(0, totalWidth);
            int left = (totalWidth - text.Length) / 2;
            int right = totalWidth - text.Length - left;
            return new string(' ', left) + text + new string(' ', right);
        }
    }
}
