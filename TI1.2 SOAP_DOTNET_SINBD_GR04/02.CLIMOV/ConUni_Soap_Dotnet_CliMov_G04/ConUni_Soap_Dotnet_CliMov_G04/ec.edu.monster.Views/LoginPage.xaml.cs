using System;
using Microsoft.Maui.Controls;

namespace ConUni_Soap_Dotnet_CliMov_G04.Views
{
    public partial class LoginPage : ContentPage
    {
        // Credenciales quemadas
        private const string LOGIN_USER = "MONSTER";
        private const string LOGIN_PASS = "monster9";

        public LoginPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (UserEntry != null)
                UserEntry.Text = string.Empty;

            if (PassEntry != null)
                PassEntry.Text = string.Empty;

            if (ErrorLabel != null)
                ErrorLabel.Text = string.Empty;
        }


        // Firma correcta: void (object sender, EventArgs e)
        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var user = UserEntry?.Text?.Trim() ?? string.Empty;
            var pass = PassEntry?.Text?.Trim() ?? string.Empty;

            if (user == LOGIN_USER && pass == LOGIN_PASS)
            {
                // Navegar a Home
                await Shell.Current.GoToAsync("//home");
            }
            else
            {
                // Mensaje simple de error (si tienes un Label ErrorLabel en XAML)
                if (ErrorLabel != null)
                {
                    ErrorLabel.Text = "Usuario o contraseña inválidos.";
                }
                else
                {
                    await DisplayAlert("Login", "Usuario o contraseña inválidos.", "OK");
                }
            }
        }
    }
}
