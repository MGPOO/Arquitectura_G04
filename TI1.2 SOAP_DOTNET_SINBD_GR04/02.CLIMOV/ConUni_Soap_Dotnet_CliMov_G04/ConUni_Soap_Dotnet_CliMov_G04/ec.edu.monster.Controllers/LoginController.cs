namespace ConUni_Soap_Dotnet_CliMov_G04.Controllers
{
    public class LoginController
    {
        private const string USER = "MONSTER";
        private const string PASS = "monster9";

        public bool ValidateLogin(string username, string password)
        {
            return username == USER && password == PASS;
        }
    }
}
