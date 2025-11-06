using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ClienteRestWpf.Models;

namespace ClienteRestWpf.Views
{
    public partial class ConversorWindow : Window
    {
        private readonly ApiConversorClient _api = new ApiConversorClient();

        // Mapa de conversiones permitidas
        private readonly Dictionary<string, string[]> _longitudTargets = new()
        {
            ["Centímetros"] = new[] { "Pies", "Pulgadas" },
            ["Pies"] = new[] { "Centímetros" },
            ["Pulgadas"] = new[] { "Centímetros" },
            ["Metros"] = new[] { "Yardas" },
            ["Yardas"] = new[] { "Metros" },
        };

        private readonly Dictionary<string, string[]> _masaTargets = new()
        {
            ["Kilogramos"] = new[] { "Libras" },
            ["Libras"] = new[] { "Kilogramos" },
            ["Gramos"] = new[] { "Onzas" },
            ["Onzas"] = new[] { "Gramos" },
        };

        private readonly Dictionary<string, string[]> _tempTargets = new()
        {
            ["Celsius"] = new[] { "Fahrenheit", "Kelvin" },
            ["Fahrenheit"] = new[] { "Celsius", "Kelvin" },
            ["Kelvin"] = new[] { "Celsius", "Fahrenheit" },
        };

        // Mapa de abreviaturas
        private static readonly Dictionary<string, string> UnitAbbrev =
            new(StringComparer.OrdinalIgnoreCase)
            {
                // Longitud
                ["centímetros"] = "cm",
                ["pulgadas"] = "in",
                ["pies"] = "ft",
                ["metros"] = "m",
                ["yardas"] = "yd",

                // Masa
                ["kilogramos"] = "kg",
                ["libras"] = "lb",
                ["gramos"] = "g",
                ["onzas"] = "oz",

                // Temperatura
                ["celsius"] = "°C",
                ["fahrenheit"] = "°F",
                ["kelvin"] = "K",
            };

        public ConversorWindow(string userName)
        {
            InitializeComponent();
            HelloText.Text = $"Hola, {userName}";
            InitCombos();
        }

        private void InitCombos()
        {
            CmbLongDe.ItemsSource = _longitudTargets.Keys.ToList();
            CmbLongDe.SelectedItem = "Centímetros";
            UpdateTargets(CmbLongDe, CmbLongA, _longitudTargets);

            CmbMasaDe.ItemsSource = _masaTargets.Keys.ToList();
            CmbMasaDe.SelectedItem = "Kilogramos";
            UpdateTargets(CmbMasaDe, CmbMasaA, _masaTargets);

            CmbTempDe.ItemsSource = _tempTargets.Keys.ToList();
            CmbTempDe.SelectedItem = "Celsius";
            UpdateTargets(CmbTempDe, CmbTempA, _tempTargets);
        }

        private static void UpdateTargets(ComboBox cmbDe, ComboBox cmbA, Dictionary<string, string[]> map)
        {
            var de = cmbDe.SelectedItem as string;
            cmbA.ItemsSource = map.TryGetValue(de!, out var list) ? list : Array.Empty<string>();
            cmbA.SelectedItem = (cmbA.Items.Count > 0) ? cmbA.Items[0] : null;
        }

        private void CmbLongDe_SelectionChanged(object sender, SelectionChangedEventArgs e)
            => UpdateTargets(CmbLongDe, CmbLongA, _longitudTargets);

        private void CmbMasaDe_SelectionChanged(object sender, SelectionChangedEventArgs e)
            => UpdateTargets(CmbMasaDe, CmbMasaA, _masaTargets);

        private void CmbTempDe_SelectionChanged(object sender, SelectionChangedEventArgs e)
            => UpdateTargets(CmbTempDe, CmbTempA, _tempTargets);

        private void Logout(object sender, RoutedEventArgs e)
        {
            var login = new LoginWindow();
            login.Show();
            Close();
        }

        // ---------- VALIDACIONES ----------
        private static bool TryReadAndValidate(string categoria, string? de, string? a, string? text,
                                               out double v, out string error)
        {
            error = "";
            v = 0;

            if (string.IsNullOrWhiteSpace(text))
            {
                error = "Ingresa un valor numérico.";
                return false;
            }

            if (!double.TryParse(text.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out v))
            {
                error = "El valor debe ser un número válido (usa punto decimal).";
                return false;
            }

            if (de is null || a is null)
            {
                error = "Debe seleccionar origen y destino.";
                return false;
            }

            switch (categoria)
            {
                case "Longitud":
                    if (v <= 0) { error = "El valor debe ser > 0."; return false; }
                    break;

                case "Masa":
                    if (v < 0) { error = "El valor debe ser ≥ 0."; return false; }
                    break;

                case "Temperatura":
                    if (de == "Celsius" && a == "Kelvin" && v < -273.15)
                    { error = "Celsius no puede ser menor que -273.15 °C."; return false; }

                    if (de == "Kelvin" && (a == "Celsius" || a == "Fahrenheit") && v < 0)
                    { error = "Kelvin no puede ser negativo."; return false; }

                    if (de == "Fahrenheit" && a == "Kelvin" && v < -459.67)
                    { error = "Fahrenheit no puede ser menor que -459.67 °F."; return false; }
                    break;
            }

            return true;
        }

        // ---------- Resultado formateado ----------
        private static string Pretty(ConversionResponse? r, int decimals = 3)
        {
            if (r is null) return "Sin respuesta";

            string ab(string u) => UnitAbbrev.TryGetValue(u, out var a) ? a : u;
            string num(double x) => Math.Round(x, decimals).ToString($"0.{new string('#', decimals)}", CultureInfo.InvariantCulture);

            return $"{num(r.ValorIngresado)} {ab(r.DeUnidad)} = {num(r.Resultado)} {ab(r.AUnidad)}";
        }

        // -------------------- LONGITUD --------------------
        private async void CalcularLongitud(object sender, RoutedEventArgs e)
        {
            if (!TryReadAndValidate("Longitud", CmbLongDe.Text, CmbLongA.Text, TxtLongValor.Text, out var v, out var err))
            { LblLongResultado.Text = err; return; }

            try
            {
                var resp = await _api.GetAsync(PairToEndpointLongitud(CmbLongDe.Text, CmbLongA.Text), v);
                LblLongResultado.Text = Pretty(resp);
            }
            catch (Exception ex) { LblLongResultado.Text = "Error: " + ex.Message; }
        }

        // -------------------- MASA --------------------
        private async void CalcularMasa(object sender, RoutedEventArgs e)
        {
            if (!TryReadAndValidate("Masa", CmbMasaDe.Text, CmbMasaA.Text, TxtMasaValor.Text, out var v, out var err))
            { LblMasaResultado.Text = err; return; }

            try
            {
                var resp = await _api.GetAsync(PairToEndpointMasa(CmbMasaDe.Text, CmbMasaA.Text), v);
                LblMasaResultado.Text = Pretty(resp);
            }
            catch (Exception ex) { LblMasaResultado.Text = "Error: " + ex.Message; }
        }

        // -------------------- TEMPERATURA --------------------
        private async void CalcularTemperatura(object sender, RoutedEventArgs e)
        {
            if (!TryReadAndValidate("Temperatura", CmbTempDe.Text, CmbTempA.Text, TxtTempValor.Text, out var v, out var err))
            { LblTempResultado.Text = err; return; }

            try
            {
                var resp = await _api.GetAsync(PairToEndpointTemp(CmbTempDe.Text, CmbTempA.Text), v);
                LblTempResultado.Text = Pretty(resp);
            }
            catch (Exception ex) { LblTempResultado.Text = "Error: " + ex.Message; }
        }

        // ---------- Endpoints ----------
        private static string PairToEndpointLongitud(string de, string a) => (de, a) switch
        {
            ("Centímetros", "Pies") => "centimetros-a-pies",
            ("Pies", "Centímetros") => "pies-a-centimetros",
            ("Pulgadas", "Centímetros") => "pulgadas-a-centimetros",
            ("Centímetros", "Pulgadas") => "centimetros-a-pulgadas",
            ("Metros", "Yardas") => "metros-a-yardas",
            ("Yardas", "Metros") => "yardas-a-metros",
            _ => throw new InvalidOperationException("Conversión no permitida.")
        };

        private static string PairToEndpointMasa(string de, string a) => (de, a) switch
        {
            ("Kilogramos", "Libras") => "kilogramos-a-libras",
            ("Libras", "Kilogramos") => "libras-a-kilogramos",
            ("Gramos", "Onzas") => "gramos-a-onzas",
            ("Onzas", "Gramos") => "onzas-a-gramos",
            _ => throw new InvalidOperationException("Conversión no permitida.")
        };

        private static string PairToEndpointTemp(string de, string a) => (de, a) switch
        {
            ("Celsius", "Fahrenheit") => "celsius-a-fahrenheit",
            ("Fahrenheit", "Celsius") => "fahrenheit-a-celsius",
            ("Celsius", "Kelvin") => "celsius-a-kelvin",
            ("Kelvin", "Celsius") => "kelvin-a-celsius",
            ("Fahrenheit", "Kelvin") => "fahrenheit-a-kelvin",
            ("Kelvin", "Fahrenheit") => "kelvin-a-fahrenheit",
            _ => throw new InvalidOperationException("Conversión no permitida.")
        };
    }
}
