using System.Text.RegularExpressions;
using System.Windows;
using ConUni_Soap_Dotnet_CliEsc_G04.Controllers;

namespace ConUni_Soap_Dotnet_CliEsc_G04.Views
{
    public partial class LoginView : Window
    {
        private readonly AccountController _account;

        public LoginView(AccountController account)
        {
            InitializeComponent();
            _account = account;
        }

        private void OnLogin(object sender, RoutedEventArgs e)
        {
            string username = UserBox.Text.Trim();
            string password = PassBox.Password.Trim();

            // ========= VALIDACIONES ========

            if (string.IsNullOrEmpty(username))
            {
                ShowError("El usuario es obligatorio.");
                return;
            }

            if (username.Length < 3)
            {
                ShowError("El usuario debe tener al menos 3 caracteres.");
                return;
            }

            // Solo letras, números, punto, guion y guion bajo
            if (!Regex.IsMatch(username, @"^[A-Za-z0-9._-]+$"))
            {
                ShowError("El usuario solo puede contener letras, números, punto, guion y guion bajo.");
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                ShowError("La contraseña es obligatoria.");
                return;
            }

            // Contraseña con al menos una letra y un número
            if (!Regex.IsMatch(password, @"^(?=.*[A-Za-z])(?=.*\d).+$"))
            {
                ShowError("La contraseña debe contener al menos una letra y un número.");
                return;
            }

            // ========= VALIDACIÓN DE LOGIN REAL =========
            if (_account.Login(username, password))
            {
                DialogResult = true;
                Close();
            }
            else
            {
                ShowError("Usuario o contraseña incorrectos.");
            }
        }

        private void ShowError(string message)
        {
            ErrorText.Text = message;
            ErrorText.Visibility = Visibility.Visible;
        }
    }
}
