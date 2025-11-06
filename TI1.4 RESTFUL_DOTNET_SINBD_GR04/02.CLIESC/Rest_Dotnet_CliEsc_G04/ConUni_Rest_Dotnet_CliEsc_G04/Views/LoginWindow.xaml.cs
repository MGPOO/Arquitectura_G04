using System.Windows;

namespace ClienteRestWpf.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void OnLogin(object sender, RoutedEventArgs e)
        {
            var error = ValidateLogin();
            if (error is not null)
            {
                ErrorText.Text = error;
                ErrorText.Visibility = Visibility.Visible;
                return;
            }

            // Credenciales válidas
            var win = new ConversorWindow("MONSTER");
            win.Show();
            Close();
        }

        private string? ValidateLogin()
        {
            ErrorText.Visibility = Visibility.Collapsed;

            var user = (UserBox.Text ?? "").Trim();
            var pass = (PassBox.Password ?? "").Trim();

            if (string.IsNullOrEmpty(user))
                return "Ingresa tu usuario.";
            if (string.IsNullOrEmpty(pass))
                return "Ingresa tu contraseña.";
            if (user.Length < 3)
                return "El usuario debe tener al menos 3 caracteres.";
            if (pass.Length < 6)
                return "La contraseña debe tener al menos 6 caracteres.";

            // Credenciales “quemadas”
            if (!(user == "MONSTER" && pass == "monster9"))
                return "Usuario o contraseña incorrectos.";

            return null;
        }
    }
}
