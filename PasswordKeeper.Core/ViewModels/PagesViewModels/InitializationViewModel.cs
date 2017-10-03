using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace PasswordKeeper.Core
{
    public class InitializationViewModel : BaseViewModel
    {
        #region Password Requirements
        private bool _hasOneLowerCase = false;
        public bool HasOneLowerCase
        {
            get => _hasOneLowerCase;
            set
            {
                _hasOneLowerCase = value;
                OnPropertyChanged(nameof(HasOneLowerCase));
            }
        }

        private bool _hasOneUpperCase = false;
        public bool HasOneUpperCase
        {
            get => _hasOneUpperCase;
            set
            {
                _hasOneUpperCase = value;
                OnPropertyChanged(nameof(HasOneUpperCase));
            }
        }

        private bool _hasOneSpecialCharacter = false;
        public bool HasOneSpecialCharacter
        {
            get => _hasOneSpecialCharacter;
            set
            {
                _hasOneSpecialCharacter = value;
                OnPropertyChanged(nameof(HasOneSpecialCharacter));
            }
        }

        private bool _hasLengthBetween8to15chars = false;
        public bool HasLengthBetween8to15chars
        {
            get => _hasLengthBetween8to15chars;
            set
            {
                _hasLengthBetween8to15chars = value;
                OnPropertyChanged(nameof(HasLengthBetween8to15chars));
            }
        }

        private bool _passwordMatch = false;
        public bool PasswordMatch
        {
            get => _passwordMatch;
            set
            {
                _passwordMatch = value;
                OnPropertyChanged(nameof(PasswordMatch));
            }
        }


        #endregion

        #region Constructors
        public InitializationViewModel()
        {
            Proceed = new RelayCommand(x => CloseAndProceed(), x => _passwordsAreValid);
            PasswordMeetsRequirements = false;
        }
        #endregion

        #region Memebers

        private SecureString _retypedPassword = new SecureString();
        private SecureString _password = new SecureString();
        private char[] _specialChars = "[$&+,:;=?@#|'<>.^*()%!-]".ToCharArray();
        private bool _passwordsAreValid = false;

        private bool _passwordMeetsRequirements;
        public bool PasswordMeetsRequirements
        {
            get
            {
                return _passwordMeetsRequirements;
            }
            private set
            {
                _passwordMeetsRequirements = value;
                OnPropertyChanged(nameof(PasswordMeetsRequirements));
            }
        }
        #endregion

        #region Commands and Methods
        public ICommand Proceed { get; private set; }

        private void CloseAndProceed()
        {
            ApplicationViewModel appViewModel = IoC.Get<ApplicationViewModel>();
            appViewModel.CreateNewVault(_password);
            appViewModel.GoToPage(ApplicationPage.MainPage);
            appViewModel.SideMenuVisible = true;
        }
        #endregion

        public void SetAndCheckPassword(SecureString password)
        {
            _password = password;
            ValidatePassword();
        }

        public void SetRetypedPassword(SecureString retypedPassword)
        {
            _retypedPassword = retypedPassword;
            ValidatePassword();
        }

        #region Private Methods
        private void ValidatePassword()
        {
            const int minLength = 8;
            const int maxLength = 15;
            HasLengthBetween8to15chars = _password.Length > minLength && _password.Length < maxLength ? true : false;
            string passwordUnsercured = _password.Unsecure();
            HasOneLowerCase = ContainsOneLowerCase(passwordUnsercured);
            HasOneUpperCase = ContainsUpperCase(passwordUnsercured);
            HasOneSpecialCharacter = ConstainsSpecialChar(passwordUnsercured);
            PasswordMeetsRequirements = HasLengthBetween8to15chars &&
                    HasOneLowerCase &&
                    HasOneUpperCase &&
                    HasOneSpecialCharacter;
            CompareMainAndRetypedPassword();
            _passwordsAreValid = PasswordMeetsRequirements && PasswordMatch;
        }

        private bool ContainsOneLowerCase(string password)
        {
            foreach (char c in password)
                if (char.IsLower(c))
                    return true;
            return false;
        }

        private bool ContainsUpperCase(string password)
        {
            foreach (char c in password)
                if (char.IsUpper(c))
                    return true;
            return false;
        }

        private bool ConstainsSpecialChar(string password)
        {
            foreach (char c in password)
                for (int i = 0; i < _specialChars.Length; i++)
                    if (c == _specialChars[i])
                        return true;
            return false;
        }

        private void CompareMainAndRetypedPassword()
        {
            if (_password.Length == 0 || _retypedPassword.Length == 0)
            {
                PasswordMatch = false;
                return;
            }
            string mainPassword = _password.Unsecure();
            string retypedPassword = _retypedPassword.Unsecure();
            if (mainPassword == retypedPassword)
                PasswordMatch = true;
            else
                PasswordMatch = false;
        }
        #endregion
    }
}
