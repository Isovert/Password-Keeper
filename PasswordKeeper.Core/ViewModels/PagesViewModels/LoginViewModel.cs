using System;
using System.Security;
using System.Windows.Input;

namespace PasswordKeeper.Core
{
    public class LoginViewModel : BaseViewModel
    {
        private SecureString _password = new SecureString();
        private int maxLoginAttempts = 3;
        private int loginAttempt = 0;

        public LoginViewModel()
        {
            Login = new RelayCommand(x => LoginMethod(), x => _password.Length > 0 ? true : false);
        }

        public ICommand Login { get; set; }

        public void SetPassword(SecureString password)
        {
            _password = password;
        }

        private void LoginMethod()
        {
            try
            {
                IoC.Get<ApplicationViewModel>().LoadVault(_password);
                IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.MainPage);
                IoC.Get<ApplicationViewModel>().SideMenuVisible = true;
            }
            catch (UnauthorizedAccessException ex)
            {
                loginAttempt++;
                //TODO - show invalid password message
                if (loginAttempt == maxLoginAttempts)
                    //TODO - say goodbye and close application
                    ;
            }
        }
    }
}
