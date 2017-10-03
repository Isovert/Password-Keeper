﻿using System;
using System.Diagnostics;
using System.Windows.Input;

namespace PasswordKeeper.Core
{
    public class LoginTreeViewModel : BaseTreeViewModel
    {
        private CredentialsModel _loginModel;
        private FolderTreeViewModel _parentFolderTreeViewModel;
        private bool unsavedChanges = false;

        public LoginTreeViewModel(CredentialsModel loginModel, FolderTreeViewModel folderTreeViewModel)
        {
            _loginModel = loginModel;
            _name = _loginModel.Name;
            _login = _loginModel.Login;
            _url = _loginModel.URL;
            _notes = _loginModel.Notes;
            _parentFolderTreeViewModel = folderTreeViewModel;
            OpenURL = new RelayCommand(x => OpenURLMethod(), x => URL.Length > 0 ? true : false);
            CopyPasswordToClipboard = new RelayCommand(x => this._loginModel.CopyPasswordToClipboard());
            SaveDataToModel = new RelayCommand(x => SaveChanges(), x => unsavedChanges ? true : false);
            SetNewPassword = new RelayCommand(x => SetNewPasswordMethod());
        }

        #region RelayCommands
        public ICommand OpenURL { get; private set; }
        public ICommand CopyPasswordToClipboard { get; private set; }
        public ICommand SaveDataToModel { get; private set; }
        public ICommand SetNewPassword { get; private set; }
        #endregion

        #region Presentation Members
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    if (_isSelected == true)
                    {
                        //Deletion
                        //_mainWindowViewModelObserver.ActiveTreeLoginViewModel = this;
                    }
                    else
                    {
                        FocusLost();
                        //Deletion
                        //_mainWindowViewModelObserver.ActiveTreeLoginViewModel = null;
                    }
                    OnPropertyChanged("IsSelected");
                }
            }
        }
        #endregion

        #region Methods
        private void FocusLost()
        {
            if (unsavedChanges == true)
            {
                //Deletion
                //MessageBoxResult MBR = MessageBox.Show("Save changes?", "Save changes?", MessageBoxButton.YesNo);
                //if (MBR == MessageBoxResult.Yes)
                //{
                //    SaveChanges();
                //}
                //else
                //{
                //    RestoreDefaults();
                //}
            }
        }

        private void RestoreDefaults()
        {
            Name = _loginModel.Name;
            Login = _loginModel.Login;
            URL = _loginModel.URL;
            Notes = _loginModel.Notes;
            unsavedChanges = false;
        }

        private void SaveChanges()
        {
            _loginModel.Name = _name;
            _loginModel.Login = _login;
            _loginModel.URL = _url;
            _loginModel.Notes = _notes;
            ModificationDateTime = DateTime.Now;
            unsavedChanges = false;
            //Deletion
            //_mainWindowViewModelObserver.UnsavedChanges = true;
        }

        private void SetNewPasswordMethod()
        {
            //Deletion
            //GetPasswordWindow getPasswordWindow = new GetPasswordWindow();
            //getPasswordWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            //getPasswordWindow.Owner = _mainWindowViewModelObserver.parentWindow;
            //getPasswordWindow.ShowDialog();
            //if (!getPasswordWindow.Valid)
            //    return;
            //_loginModel.SavePassword(getPasswordWindow.Password);
            //ModificationDateTime = DateTime.Now;
        }

        private void OpenURLMethod()
        {
            try
            {
                Process.Start(URL);
            }
            catch
            {
                //Deletion
                //MessageBox.Show("Could not resolve URL link: " + URL);
            }
        }

        public void Delete()
        {
            _parentFolderTreeViewModel.DeleteLogin(_loginModel, this);
        }

        #endregion Methods
        
        #region Properties
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                unsavedChanges = true;
                OnPropertyChanged("Name");
            }
        }

        private string _login;
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                unsavedChanges = true;
                OnPropertyChanged("Login");
            }
        }

        private string _url;
        public string URL
        {
            get { return _url; }
            set
            {
                _url = value;
                unsavedChanges = true;
                OnPropertyChanged("URL");
            }
        }

        private string _notes;
        public string Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
                unsavedChanges = true;
                OnPropertyChanged("Notes");
            }
        }

        public DateTime CreationDate
        {
            get { return _loginModel.CreationDateTime; }
        }

        public DateTime ModificationDateTime
        {
            get { return _loginModel.ModificationDateTime; }
            private set
            {
                _loginModel.ModificationDateTime = value;
                OnPropertyChanged("ModificationDateTime");
            }
        }
        #endregion Properties
    }
}
