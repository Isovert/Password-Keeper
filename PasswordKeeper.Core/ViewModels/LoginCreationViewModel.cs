using System.ComponentModel;
using System.Windows.Input;

namespace PasswordKeeper.Core
{
    class LoginCreationViewModel : INotifyPropertyChanged
    {
        public LoginCreationViewModel(LoginModel loginModel)
        {
            _loginModel = loginModel;
            Save = new RelayCommand(x => SaveMethod(), x => DataIsValid() ? true : false);
        }

        private LoginModel _loginModel;

        #region RelayCommands
        public ICommand Save { get; private set; }
        #endregion

        #region Methods

        private void SaveMethod()
        {
            _loginModel.SavePassword(_temporalPasswordContainer);
            _temporalPasswordContainer = null;
        }

        private bool DataIsValid()
        {
            if (string.IsNullOrEmpty(Name))
                return false;
            return true;
        }

        #endregion

        public string Name
        {
            get { return _loginModel.Name; }
            set { _loginModel.Name = value; OnPropertyChanged("Name"); }
        }

        public string Login
        {
            get { return _loginModel.Login; }
            set { _loginModel.Login = value; OnPropertyChanged("LOGIN"); }
        }

        public string Notes
        {
            get { return _loginModel.Notes; }
            set { _loginModel.Notes = value; OnPropertyChanged("Notes"); }
        }

        public string _temporalPasswordContainer = "";
        public string TemporalPasswordContainer
        {
            set { _temporalPasswordContainer = value; }
        }

        public string URL
        {
            get { return _loginModel.URL; }
            set { _loginModel.URL = value; OnPropertyChanged("URL"); }
        }

        public event PropertyChangedEventHandler PropertyChanged = null;
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
