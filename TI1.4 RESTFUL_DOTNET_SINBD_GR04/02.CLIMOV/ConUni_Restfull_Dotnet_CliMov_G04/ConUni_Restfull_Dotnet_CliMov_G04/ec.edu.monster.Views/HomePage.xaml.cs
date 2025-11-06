using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;
using ConUni_Soap_Dotnet_CliMov_G04.Controllers;

namespace ConUni_Soap_Dotnet_CliMov_G04.Views
{
    public partial class HomePage : ContentPage
    {
        // Listas completas de unidades (sin restricciones)
        private readonly List<string> _lengthUnits = new() { "Metros", "Yardas", "Centímetros", "Pulgadas", "Pies" };
        private readonly List<string> _massUnits = new() { "Kilogramos", "Libras", "Gramos", "Onzas" };
        private readonly string[] _temps = { "Celsius", "Fahrenheit", "Kelvin" };

        public HomePage()
        {
            InitializeComponent();

            Shell.SetNavBarIsVisible(this, false);

            // Forzar tema claro
            Application.Current!.UserAppTheme = AppTheme.Light;

            // Longitud
            pickerLenFrom.ItemsSource = _lengthUnits;
            pickerLenFrom.SelectedIndex = 0;
            OnLenFromChanged(pickerLenFrom, EventArgs.Empty); // llena el "A:" excluyendo el "De:"

            // Masa
            pickerMassFrom.ItemsSource = _massUnits;
            pickerMassFrom.SelectedIndex = 0;
            OnMassFromChanged(pickerMassFrom, EventArgs.Empty);

            // Temperatura (libre)
            pickerTempFrom.ItemsSource = _temps;
            pickerTempTo.ItemsSource = _temps;
            pickerTempFrom.SelectedIndex = 0;
            pickerTempTo.SelectedIndex = 1;
        }

        // =================== LONGITUD ===================
        private void OnLenFromChanged(object sender, EventArgs e)
        {
            if (pickerLenFrom.SelectedItem is not string from) return;

            // todas las unidades menos la elegida en "De:"
            var targets = _lengthUnits.Where(u => u != from).ToList();
            pickerLenTo.ItemsSource = targets;
            pickerLenTo.SelectedIndex = targets.Count > 0 ? 0 : -1;
        }

        private void OnCalcLengthClicked(object sender, EventArgs e)
        {
            if (!double.TryParse(entryLenValue.Text, out var val))
            {
                ShowError(lblLenResult, "Ingrese un valor numérico válido.");
                return;
            }

            if (pickerLenFrom.SelectedItem is not string from ||
                pickerLenTo.SelectedItem is not string to)
            {
                ShowError(lblLenResult, "Seleccione unidades.");
                return;
            }

            if (from == to)
            {
                ShowError(lblLenResult, "Las unidades deben ser diferentes.");
                return;
            }

            double result = (from, to) switch
            {
                ("Metros", "Yardas") => ConversionController.MetersToYards(val),
                ("Yardas", "Metros") => ConversionController.YardsToMeters(val),
                ("Centímetros", "Pulgadas") => ConversionController.CentimetersToInches(val),
                ("Pulgadas", "Centímetros") => ConversionController.InchesToCentimeters(val),
                ("Centímetros", "Pies") => ConversionController.CentimetersToFeet(val),
                ("Pies", "Centímetros") => ConversionController.FeetToCentimeters(val),
                _ => double.NaN
            };

            ShowOk(lblLenResult, double.IsNaN(result)
                ? "Conversión no soportada."
                : $"{val:0.####} {from} = {result:0.####} {to}");
        }

        // =================== MASA ===================
        private void OnMassFromChanged(object sender, EventArgs e)
        {
            if (pickerMassFrom.SelectedItem is not string from) return;

            var targets = _massUnits.Where(u => u != from).ToList();
            pickerMassTo.ItemsSource = targets;
            pickerMassTo.SelectedIndex = targets.Count > 0 ? 0 : -1;
        }

        private void OnCalcMassClicked(object sender, EventArgs e)
        {
            if (!double.TryParse(entryMassValue.Text, out var val))
            {
                ShowError(lblMassResult, "Ingrese un valor numérico válido.");
                return;
            }

            if (pickerMassFrom.SelectedItem is not string from ||
                pickerMassTo.SelectedItem is not string to)
            {
                ShowError(lblMassResult, "Seleccione unidades.");
                return;
            }

            if (from == to)
            {
                ShowError(lblMassResult, "Las unidades deben ser diferentes.");
                return;
            }

            double result = (from, to) switch
            {
                ("Kilogramos", "Libras") => ConversionController.KilogramsToPounds(val),
                ("Libras", "Kilogramos") => ConversionController.PoundsToKilograms(val),
                ("Gramos", "Onzas") => ConversionController.GramsToOunces(val),
                ("Onzas", "Gramos") => ConversionController.OuncesToGrams(val),
                _ => double.NaN
            };

            ShowOk(lblMassResult, double.IsNaN(result)
                ? "Conversión no soportada."
                : $"{val:0.####} {from} = {result:0.####} {to}");
        }

        // =================== TEMPERATURA ===================
        private void OnTempFromChanged(object sender, EventArgs e)
        {
            if (pickerTempFrom.SelectedItem is not string from) return;

            // todas las unidades excepto la seleccionada
            var targets = _temps.Where(u => u != from).ToList();
            pickerTempTo.ItemsSource = targets;
            pickerTempTo.SelectedIndex = targets.Count > 0 ? 0 : -1;
        }

        private void OnCalcTempClicked(object sender, EventArgs e)
        {
            if (!double.TryParse(entryTempValue.Text, out var val))
            {
                ShowError(lblTempResult, "Ingrese un valor numérico válido.");
                return;
            }

            if (pickerTempFrom.SelectedItem is not string from ||
                pickerTempTo.SelectedItem is not string to)
            {
                ShowError(lblTempResult, "Seleccione unidades.");
                return;
            }

            if (from == to)
            {
                ShowError(lblTempResult, "Las unidades deben ser diferentes.");
                return;
            }

            double result = (from, to) switch
            {
                ("Celsius", "Fahrenheit") => ConversionController.CelsiusToFahrenheit(val),
                ("Fahrenheit", "Celsius") => ConversionController.FahrenheitToCelsius(val),
                ("Celsius", "Kelvin") => ConversionController.CelsiusToKelvin(val),
                ("Kelvin", "Celsius") => ConversionController.KelvinToCelsius(val),
                ("Fahrenheit", "Kelvin") => ConversionController.FahrenheitToKelvin(val),
                ("Kelvin", "Fahrenheit") => ConversionController.KelvinToFahrenheit(val),
                _ => double.NaN
            };

            ShowOk(lblTempResult, double.IsNaN(result)
                ? "Conversión no soportada."
                : $"{val:0.####} {from} = {result:0.####} {to}");
        }


        // =================== HELPERS ===================
        private void ShowError(Label target, string msg)
        {
            target.Text = msg;
            target.TextColor = Colors.Red;
        }

        private void ShowOk(Label target, string msg)
        {
            target.Text = msg;
            target.TextColor = (Color)Application.Current!.Resources["Primary"];
        }

        // =================== LOGOUT ===================
        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//login");

        }

    }
}
