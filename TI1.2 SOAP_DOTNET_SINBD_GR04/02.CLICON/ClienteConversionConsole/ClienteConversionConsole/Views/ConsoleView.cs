using System;
using System.Globalization;
using System.Text;

namespace ClienteConversionConsoleMvc.Views
{
    public class ConsoleView
    {
        readonly ConsoleColor ColHeader = ConsoleColor.Blue;
        readonly ConsoleColor ColMenu = ConsoleColor.Green;
        readonly ConsoleColor ColInfo = ConsoleColor.White;
        readonly ConsoleColor ColOk = ConsoleColor.Green;
        readonly ConsoleColor ColWarn = ConsoleColor.DarkYellow;

        // ====== Header y Menús ======
        public void DrawHeader(string title)
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

        public void DrawMenu(string[] items)
        {
            for (int i = 0; i < items.Length; i++)
                WriteLineC("  " + (i + 1) + ". " + items[i], ColMenu);
            Console.WriteLine();
        }

        public int ReadOption(int min, int max)
        {
            while (true)
            {
                Write("Elige una opción [" + min + "-" + max + "]: ");
                string s = Console.ReadLine();
                int op;
                if (int.TryParse(s, out op) && op >= min && op <= max) return op;
                WriteWarn("Opción inválida. Intenta de nuevo.");
            }
        }

        public double ReadDouble(string prompt)
        {
            while (true)
            {
                Write(prompt + " ");
                string s = Console.ReadLine();
                if (s == null) s = "";
                s = s.Trim().Replace(',', '.');
                double v;
                if (double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out v)) return v;
                WriteWarn("Número inválido. Ej: 12,5 | 12.5 | -3 | 0.75");
            }
        }

        public string ReadPassword()
        {
            var sb = new StringBuilder();
            while (true)
            {
                var k = Console.ReadKey(true);
                if (k.Key == ConsoleKey.Enter) { Console.WriteLine(); return sb.ToString(); }
                if (k.Key == ConsoleKey.Backspace && sb.Length > 0) { sb.Length--; Console.Write("\b \b"); }
                else if (!char.IsControl(k.KeyChar)) { sb.Append(k.KeyChar); Console.Write('*'); }
            }
        }

        // ====== Mensajes ======
        public void Write(string t) { WriteC(t, ColInfo); }
        public void WriteInfo(string t) { WriteLineC(t, ColInfo); }
        public void WriteOk(string t) { WriteLineC(t, ColOk); }
        public void WriteWarn(string t) { WriteLineC(t, ColWarn); }

        public void Pause() { WriteInfo("\nPresiona una tecla para continuar…"); Console.ReadKey(true); }
        public void PauseShort() { WriteInfo("\nPresiona una tecla para continuar…"); Console.ReadKey(true); }

        public void WriteError(Exception ex)
        {
            var prev = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n⚠ Error: " + ex.Message);
            if (ex.InnerException != null) Console.WriteLine("   Detalle: " + ex.InnerException.Message);
            Console.ForegroundColor = prev;
        }

        // ====== Helpers de impresión ======
        void WriteLineC(string text, ConsoleColor color)
        {
            var p = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = p;
        }

        public void WriteC(string text, ConsoleColor color)
        {
            var p = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = p;
        }

        string PadCenter(string text, int totalWidth)
        {
            if (text.Length >= totalWidth) return text.Substring(0, totalWidth);
            int left = (totalWidth - text.Length) / 2;
            int right = totalWidth - text.Length - left;
            return new string(' ', left) + text + new string(' ', right);
        }
        public void ShowLigaLogoLDU(int width = 60)
        {
            try { Console.OutputEncoding = Encoding.UTF8; } catch { /* ignore */ }

            string[] rows = new[]
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

        // Dibuja una fila aplicando color por celda
        void DrawPatternRow(string pattern, int cellWidth,
                            ConsoleColor colStar, ConsoleColor colBlue,
                            ConsoleColor colRed, ConsoleColor colWhite)
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
                else // V
                {
                    Console.Write(new string(' ', cellWidth));
                }
            }

            Console.ForegroundColor = prev;
        }

        // Centrar texto con color
        public void WriteCentered(string text, ConsoleColor color, int totalWidth)
        {
            int pad = Math.Max(0, (totalWidth - text.Length) / 2);
            var prev = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(new string(' ', pad) + text);
            Console.ForegroundColor = prev;
        }
    }
}
