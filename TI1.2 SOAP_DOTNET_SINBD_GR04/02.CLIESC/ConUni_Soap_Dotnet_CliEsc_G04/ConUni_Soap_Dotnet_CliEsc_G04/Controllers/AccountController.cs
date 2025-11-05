namespace ConUni_Soap_Dotnet_CliEsc_G04.Controllers
{
    public class AccountController
    {
        public string? CurrentUser { get; private set; }

        public bool Login(string username, string password)
        {
            const string USER = "MONSTER";
            const string PASS = "monster9";

            if (username == USER && password == PASS)
            {
                CurrentUser = USER;
                return true;
            }
            return false;
        }

        public void Logout()
        {
            CurrentUser = null;
        }
    }
}
    