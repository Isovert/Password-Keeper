using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PasswordKeeper.ViewModels
{
    class InitialPasswordViewModel : INotifyPropertyChanged
    {
        private Regex _regexLowerCase = new Regex("[a-z]");
        private Regex _regexUpperCase = new Regex("[A-Z]");
        private Regex _regexSpecialChar = new Regex("[$&+,:;=?@#|'<>.^*()%!-]");
        private Regex _regex8to15Chars = new Regex(".{8,15}");

        private bool _hasOneLowerCase = false;
        public bool HasOneLowerCase
        {
            get => _hasOneLowerCase;
            set
            {
                _hasOneLowerCase = value;
                OnPropertyChanged("HasOneLowerCase");
            }
        }

        private bool _hasOneUpperCase = false;
        public bool HasOneUpperCase
        {
            get => _hasOneUpperCase;
            set
            {
                _hasOneUpperCase = value;
                OnPropertyChanged("HasOneUpperCase");
            }
        }

        private bool _hasOneSpecialCharacter = false;
        public bool HasOneSpecialCharacter
        {
            get => _hasOneSpecialCharacter;
            set
            {
                _hasOneSpecialCharacter = value;
                OnPropertyChanged("HasOneSpecialCharacter");
            }
        }

        private bool _hasLengthBetween8to15chars = false;
        public bool HasLengthBetween8to15chars
        {
            get => _hasLengthBetween8to15chars;
            set
            {
                _hasLengthBetween8to15chars = value;
                OnPropertyChanged("HasLengthBetween8to15chars");
            }
        }

        private string _keyword = "";
        internal string Keyword
        {
            get { return _keyword; }
            set
            {
                _keyword = value;
                OnPropertyChanged("Keyword");
            }
        }

        private string _retypedPassword;
        private string _password;
        internal string Password
        {
            get { return _password; }
            private set { _password = value; }
        }
        
        internal bool Valid { get; private set; }
        private List<bool> _passwordRequirements;

        public InitialPasswordViewModel()
        {            
            Proceed = new RelayCommand(x => CloseAndProceed(), x => CanExecuteCloseAndProceed() ? true : false);
            Valid = false;
        }

        public ICommand Proceed { get; private set; }
        public Regex RegexLowerCase { get => _regexLowerCase; set => _regexLowerCase = value; }
        
        internal void SetAndCheckPassword(string password)
        {
            Password = password;
            if (_regexUpperCase.Match(password).Success)
                HasOneUpperCase = true;
            else
                HasOneUpperCase = false;

            if (_regexLowerCase.Match(password).Success)
                HasOneLowerCase = true;
            else
                HasOneLowerCase = false;

            if (_regexSpecialChar.Match(password).Success)
                HasOneSpecialCharacter = true;
            else
                HasOneSpecialCharacter = false;

            if (_regex8to15Chars.Match(password).Success)
                HasLengthBetween8to15chars = true;
            else
                HasLengthBetween8to15chars = false;
        }

        internal void SetRetypedPassword(string retypedPassword)
        {
            _retypedPassword = retypedPassword;
        }

        private void CloseAndProceed()
        {
            Valid = true;
        }

        private bool CanExecuteCloseAndProceed()
        {
            _passwordRequirements =  new List<bool>() { _hasOneLowerCase, _hasOneUpperCase,
                                                        _hasOneSpecialCharacter, _hasLengthBetween8to15chars };
            foreach (bool requirement in _passwordRequirements)
                if (requirement != true)
                    return false;
            if (_retypedPassword != _password)
                return false;
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged = null;
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
