using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace PasswordKeeper.Core
{
    internal class ChooseLanguegeViewModel : INotifyPropertyChanged
    {
        private List<LanguageModel> _languages = new List<LanguageModel>()
                                                {
                                                    new LanguageModel("English", "en"),
                                                    new LanguageModel("Polski", "pl-PL")
                                                };
        public List<LanguageModel> Languages
        {
            get { return _languages; }
        }

        private LanguageModel _selectedLanguage = null;
        public LanguageModel SelectedLanguage
        {
            get { return _selectedLanguage; }
            set
            {
                _selectedLanguage = value;
                OnPropertyChanged("SelectedLanguage");
            }
        }

        internal ChooseLanguegeViewModel()
        {
            SaveLanguage = new RelayCommand(x => SaveLanguageMethod());
        }

        #region RelayCommands
        public ICommand SaveLanguage { get; private set; }
        #endregion

        #region Methods
        private void SaveLanguageMethod()
        {
            //deletion
            //if (_selectedLanguage != null)
            //{
            //    Properties.Settings.Default.Language = _selectedLanguage.Attribute;
            //    Properties.Settings.Default.Save();
            //    string message = ResourcesHelper.Manager.GetString(ResourcesHelper.MESSAGE_LANGUAGE_CHANGE_AFTER_RESTART);
            //    MessageBox.Show(message + _selectedLanguage.Name);
            //}
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged = null;
        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
