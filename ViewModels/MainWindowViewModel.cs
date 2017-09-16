using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordKeeper.DataModel;
using System.Windows.Input;
using PasswordKeeper.Views;
using System.Windows;

namespace PasswordKeeper.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private FolderTreeViewModel _activeFolderViewModel = null;
        public FolderTreeViewModel ActiveFolderViewModel
        {
            get { return _activeFolderViewModel; }
            set
            {
                _activeFolderViewModel = value;
                OnPropertyChanged("ActiveFolderViewModel");
            }
        }

        private LoginTreeViewModel _activeTreeLoginViewModel = null;
        public LoginTreeViewModel ActiveTreeLoginViewModel
        {
            get { return _activeTreeLoginViewModel; }
            set
            {
                _activeTreeLoginViewModel = value;
                if (_activeTreeLoginViewModel != null)
                    ActivateLoginGrid = true;
                else
                    ActivateLoginGrid = false;
                OnPropertyChanged("ActiveTreeLoginViewModel");
            }
        }

        private bool _activateLoginGrid = false;
        public bool ActivateLoginGrid
        {
            get { return _activateLoginGrid; }
            set
            {
                _activateLoginGrid = value;
                OnPropertyChanged("ActivateLoginGrid");
            }
        }

        private bool _unsavedChanges = false;
        internal bool UnsavedChanges
        {
            get { return _unsavedChanges; }
            set { _unsavedChanges = value; }
        }

        public MainWindowViewModel(Window window, Database database)
        {
            parentWindow = window;
            _database = database;
            AddFolder = new RelayCommand(x => AddFolderMethod());
            DeleteFolder = new RelayCommand(x => DeleteActiveFolderMethod(), x => ActiveFolderViewModel == null ? false : true);
            AddLogin = new RelayCommand(x => AddLoginMethod(), x => ActiveFolderViewModel == null ? false : true);
            DeleteLogin = new RelayCommand(x => DeleteActiveLoginMethod(), x => ActiveTreeLoginViewModel == null ? false : true);
            SaveData = new RelayCommand(x => SaveDataMethod());
            _firstGeneration = new ObservableCollection<FolderTreeViewModel>();
            if (_database.RootFolderModels.Count != 0)
                foreach (FolderModel folderModel in _database.RootFolderModels)
                    _firstGeneration.Add(new FolderTreeViewModel(folderModel, this));
        }

        #region RelayCommands
        public ICommand AddFolder { get; private set; }
        public ICommand DeleteFolder { get; private set; }
        public ICommand AddLogin { get; private set; }
        public ICommand DeleteLogin { get; private set; }
        public ICommand SaveData { get; private set; }
        #endregion

        #region Methods
        private void AddFolderMethod()
        {
            AddFolderWindow addFolderWindow = new AddFolderWindow();
            addFolderWindow.Owner = parentWindow;
            addFolderWindow.ShowDialog();
            if (ActiveFolderViewModel == null && addFolderWindow.Valid)
            {
                _database.RootFolderModels.Add(addFolderWindow.FolderModel);
                FirstGeneration.Add(new FolderTreeViewModel(addFolderWindow.FolderModel, this));
                UnsavedChanges = true;
                return;
            }
            if (addFolderWindow.Valid)
            {
                ActiveFolderViewModel.AddFolder(addFolderWindow.FolderModel, this);
                UnsavedChanges = true;
            }
        }

        private void DeleteActiveFolderMethod()
        {
            string message = string.Format(@"Do you really want to delete ""{0}"" folder and all content inside it?", ActiveFolderViewModel.Name);
            MessageBoxResult messageBoxResult = MessageBox.Show(message, "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBoxResult == MessageBoxResult.Yes)
                ActiveFolderViewModel.DeleteFolder();
            UnsavedChanges = true;
        }

        private void AddLoginMethod()
        {
            if (ActiveFolderViewModel == null)
                return;
            AddLoginWindow addLoginWindow = new AddLoginWindow();
            addLoginWindow.Owner = parentWindow;
            addLoginWindow.ShowDialog();
            LoginModel LM = addLoginWindow.LoginModel;
            if (LM != null)
            {
                ActiveFolderViewModel.AddLogin(LM);
                UnsavedChanges = true;
            }
        }

        private void DeleteActiveLoginMethod()
        {
            string message = string.Format(@"Do you really want to delete login for ""{0}""?", ActiveTreeLoginViewModel.Name);
            MessageBoxResult messageBoxResult = MessageBox.Show(message, "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBoxResult == MessageBoxResult.Yes)
                ActiveTreeLoginViewModel.Delete();
            ActiveTreeLoginViewModel = null;
            UnsavedChanges = true;
        }

        public void SaveDataMethod()
        {
            DatabaseSerializer.SaveData(_database);
            UnsavedChanges = false;
        }

        public void DeleteFolderTreeViewModelFromRoot(FolderModel folderModel, FolderTreeViewModel folderTreeViewModel)
        {
            FirstGeneration.Remove(folderTreeViewModel);
            _database.RootFolderModels.Remove(folderModel);
            ActiveFolderViewModel = null;
        }
        #endregion

        #region Properties
        internal readonly Window parentWindow;
        private Database _database;
        private ObservableCollection<FolderTreeViewModel> _firstGeneration;
        public ObservableCollection<FolderTreeViewModel> FirstGeneration
        {
            get { return _firstGeneration; }
            set
            {
                _firstGeneration = value;
                OnPropertyChanged("FirstGeneration");
            }
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
