using System.Globalization;

namespace ConUni_Soap_Dotnet_CliCon_G04.Views;

public static class ConsoleUI
{
    // ======== Helpers de impresión ========
    public static void Header(string titulo)
    {
        Console.WriteLine(new string('-', 120));
        Console.ForegroundColor = ConsoleColor.Blue;
        WriteCentered(titulo);
        Console.ResetColor();
        Console.WriteLine(new string('-', 120));
    }

    public static void Subtitle(string titulo)
    {
        Console.ForegroundColor = ConsoleColor.White;
        WriteCentered(titulo);
        Console.ResetColor();
    }

    public static void Success(string msg)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(msg);
        Console.ResetColor();
    }

    public static void Warn(string msg)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(msg);
        Console.ResetColor();
    }

    public static void Error(string msg)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(msg);
        Console.ResetColor();
    }

    public static void Pause()
    {
        Console.WriteLine("\nPresiona cualquier tecla para continuar...");
        Console.ReadKey(true);
    }

    public static void WriteCentered(string text)
    {
        try
        {
            int width = Console.WindowWidth;
            int left = Math.Max(0, (width - text.Length) / 2);
            Console.WriteLine(new string(' ', left) + text);
        }
        catch
        {
            Console.WriteLine(text);
        }
    }

    public static string ReadPassword()
    {
        var sb = new System.Text.StringBuilder();
        ConsoleKeyInfo k;
        while ((k = Console.ReadKey(true)).Key != ConsoleKey.Enter)
        {
            if (k.Key == ConsoleKey.Backspace && sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
                Console.Write("\b \b");
            }
            else if (!char.IsControl(k.KeyChar))
            {
                sb.Append(k.KeyChar);
                Console.Write("*");
            }
        }
        Console.WriteLine();
        return sb.ToString();
    }

    /// <summary>
    /// Lee un número de la consola. Valida formato, NaN/Inf y signo según bandera.
    /// Usar punto (.) como separador decimal.
    /// </summary>
    public static double ReadDoubleStrict(string prompt, bool allowNegative)
    {
        while (true)
        {
            Console.Write(prompt);
            var raw = Console.ReadLine()?.Trim();
            raw = raw?.Replace(',', '.');

            if (double.TryParse(raw, NumberStyles.Float, CultureInfo.InvariantCulture, out double v)
                && !double.IsNaN(v) && !double.IsInfinity(v)
                && (allowNegative || v >= 0))
            {
                return v;
            }

            if (allowNegative)
                Warn("Ingresa un número válido (se permite negativo). Usa punto para decimales.");
            else
                Warn("Ingresa un número válido (0 o positivo). Usa punto para decimales.");
        }
    }

    // ======== Logo SOLO para Login ========
    public static void LogoLiga()
    {
        string[] art =
        {
            "★   ★   ★   ★   ★",
            "      ███████████      ",
            "    ███████████████    ",
            "  ██████  ███  ██████  ",
            " █████     █     █████ ",
            " ███      ███      ███ ",
            "  ███     ███     ███  ",
            "    ███         ███    ",
            "      ███████████      ",
            "         █████         ",
            "          ▓   ▓        ",
            "          ▓   ▓        ",
            "          ▓▓▓▓▓        "
        };

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        WriteCentered(art[0]); Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Blue;
        for (int i = 1; i <= 3; i++) WriteCentered(art[i]);
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Red;
        for (int i = 4; i <= 8; i++) WriteCentered(art[i]);
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Blue;
        WriteCentered(art[9]); Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.White;
        WriteCentered(art[10]); WriteCentered(art[11]); WriteCentered(art[12]);
        Console.ResetColor();

        Console.WriteLine();
    }
}
