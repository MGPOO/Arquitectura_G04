using System;
using System.Globalization;
using System.Text;

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
        var sb = new StringBuilder();
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
    public static void ShowLigaLogoLDU(int width = 60)
    {
        try { Console.OutputEncoding = Encoding.UTF8; } catch { /* ignore */ }

        string[] rows =
        {
            "V*V*V*V*V*",
            "AAAAAAAR",
            "AABAABRR",
            "AABARBRR",
            "VAABBRRV",
            "VVRRRRVV",
            "VVVRRVVV"
        };

        ConsoleColor colStar = ConsoleColor.Yellow;
        ConsoleColor colBlue = ConsoleColor.DarkBlue;
        ConsoleColor colRed = ConsoleColor.DarkRed;
        ConsoleColor colWhite = ConsoleColor.White;

        const int cellWidth = 2;

        for (int r = 0; r < rows.Length; r++)
        {
            int rowLenCells = rows[r].Length * cellWidth;
            int pad = Math.Max(0, (width - rowLenCells) / 2);
            Console.Write(new string(' ', pad));

            DrawPatternRow(rows[r], cellWidth, colStar, colBlue, colRed, colWhite);
            Console.WriteLine();
        }

        Console.WriteLine();
    }

    private static void DrawPatternRow(
        string pattern,
        int cellWidth,
        ConsoleColor colStar,
        ConsoleColor colBlue,
        ConsoleColor colRed,
        ConsoleColor colWhite)
    {
        var prev = Console.ForegroundColor;

        for (int i = 0; i < pattern.Length; i++)
        {
            char c = pattern[i];
            if (c == '*')
            {
                Console.ForegroundColor = colStar;
                Console.Write('★');
                if (cellWidth > 1) Console.Write(' ');
            }
            else if (c == 'A')
            {
                Console.ForegroundColor = colBlue;
                Console.Write(new string('█', cellWidth));
            }
            else if (c == 'R')
            {
                Console.ForegroundColor = colRed;
                Console.Write(new string('█', cellWidth));
            }
            else if (c == 'B')
            {
                Console.ForegroundColor = colWhite;
                Console.Write(new string('█', cellWidth));
            }
            else // 'V' o cualquier otro
            {
                Console.Write(new string(' ', cellWidth));
            }
        }

        Console.ForegroundColor = prev;
    }
}
