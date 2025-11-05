using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using ConUni_Soap_Dotnet_CliEsc_G04.Controllers;

namespace ConUni_Soap_Dotnet_CliEsc_G04.Views
{
    public partial class MainView : Window
    {
        private readonly AccountController _account;
        private readonly ConversorController _conv = new();

        // Pares válidos por categoría
        private readonly Dictionary<string, string[]> _longPairs = new()
        {
            ["Centímetros"] = new[] { "Pies", "Pulgadas" },
            ["Pies"] = new[] { "Centímetros" },
            ["Pulgadas"] = new[] { "Centímetros" },
            ["Metros"] = new[] { "Yardas" },
            ["Yardas"] = new[] { "Metros" },
        };

        private readonly Dictionary<string, string[]> _masaPairs = new()
        {
            ["Kilogramos"] = new[] { "Libras" },
            ["Libras"] = new[] { "Kilogramos" },
            ["Gramos"] = new[] { "Onzas" },
            ["Onzas"] = new[] { "Gramos" },
        };

        private readonly Dictionary<string, string[]> _tempPairs = new()
        {
            ["Celsius"] = new[] { "Fahrenheit", "Kelvin" },
            ["Fahrenheit"] = new[] { "Celsius", "Kelvin" },
            ["Kelvin"] = new[] { "Celsius", "Fahrenheit" },
        };

        public MainView(AccountController account)
        {
            InitializeComponent();
            _account = account;
            HolaText.Text = $"Hola, {(_account.CurrentUser ?? "Usuario")}";
            CargarCombos();
        }

        // ===== Helpers UI =====
        private void ShowError(Border box, TextBlock label, string msg, Control? focus = null)
        {
            label.Text = msg;
            box.Visibility = Visibility.Visible;

            focus?.Focus();

            // auto-ocultar en 3.5s
            var t = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(3500) };
            t.Tick += (_, __) =>
            {
                box.Visibility = Visibility.Collapsed;
                t.Stop();
            };
            t.Start();
        }

        private void ClearError(Border box, TextBlock label)
        {
            label.Text = "";
            box.Visibility = Visibility.Collapsed;
        }

        private void ClearOutputs()
        {
            LongRes.Text = MasaRes.Text = TempRes.Text = "";
            ClearError(LongErrBox, LongErr);
            ClearError(MasaErrBox, MasaErr);
            ClearError(TempErrBox, TempErr);
        }

        private static void FillDest(ComboBox de, ComboBox a, Dictionary<string, string[]> pairs)
        {
            var key = de.SelectedItem?.ToString() ?? "";
            if (!pairs.ContainsKey(key)) return;
            a.ItemsSource = pairs[key];
            a.SelectedIndex = 0;
        }

        private void CargarCombos()
        {
            LongDe.ItemsSource = _longPairs.Keys; LongDe.SelectedIndex = 0;
            LongDe.SelectionChanged += (_, __) => FillDest(LongDe, LongA, _longPairs);
            FillDest(LongDe, LongA, _longPairs);

            MasaDe.ItemsSource = _masaPairs.Keys; MasaDe.SelectedIndex = 0;
            MasaDe.SelectionChanged += (_, __) => FillDest(MasaDe, MasaA, _masaPairs);
            FillDest(MasaDe, MasaA, _masaPairs);

            TempDe.ItemsSource = _tempPairs.Keys; TempDe.SelectedIndex = 0;
            TempDe.SelectionChanged += (_, __) => FillDest(TempDe, TempA, _tempPairs);
            FillDest(TempDe, TempA, _tempPairs);
        }

        // ===== Handlers =====
        private async void OnLongitud(object sender, RoutedEventArgs e)
        {
            ClearOutputs();

            if (!double.TryParse(LongValor.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out var v) || v <= 0)
            {
                ShowError(LongErrBox, LongErr, "Ingresa un número mayor a 0.", LongValor);
                return;
            }

            try
            {
                LongRes.Text = "Calculando...";
                LongRes.Text = await _conv.ConvertirLongitudAsync(v, LongDe.Text, LongA.Text);
            }
            catch (Exception ex)
            {
                ShowError(LongErrBox, LongErr, $"Error al convertir: {ex.Message}");
            }
        }

        private async void OnMasa(object sender, RoutedEventArgs e)
        {
            ClearOutputs();

            if (!double.TryParse(MasaValor.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out var v))
            {
                ShowError(MasaErrBox, MasaErr, "Ingresa un número válido.", MasaValor);
                return;
            }
            if (v < 0)
            {
                ShowError(MasaErrBox, MasaErr, "La masa no puede ser negativa.", MasaValor);
                return;
            }

            try
            {
                MasaRes.Text = "Calculando...";
                MasaRes.Text = await _conv.ConvertirMasaAsync(v, MasaDe.Text, MasaA.Text);
            }
            catch (Exception ex)
            {
                ShowError(MasaErrBox, MasaErr, $"Error al convertir: {ex.Message}");
            }
        }

        private async void OnTemp(object sender, RoutedEventArgs e)
        {
            ClearOutputs();

            if (!double.TryParse(TempValor.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out var v))
            {
                ShowError(TempErrBox, TempErr, "Ingresa un número válido.", TempValor);
                return;
            }

            try
            {
                TempRes.Text = "Calculando...";
                TempRes.Text = await _conv.ConvertirTemperaturaAsync(v, TempDe.Text, TempA.Text);
            }
            catch (Exception ex)
            {
                ShowError(TempErrBox, TempErr, $"Error al convertir: {ex.Message}");
            }
        }

        private void OnSalir(object sender, RoutedEventArgs e)
        {
            _account.Logout();
            Close();
            Application.Current.Shutdown();
        }
    }
}
