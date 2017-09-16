using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordKeeper.DataModel;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PasswordKeeper.ViewModels
{
    class FolderTreeViewModel : BaseTreeViewModel
    {
        #region Constructors
        public FolderTreeViewModel(FolderModel folderModel, MainWindowViewModel mainWindowViewModel)
        {
            this._folderModel = folderModel;
            _mainWindowViewModelObserver = mainWindowViewModel;
            _folderViewModelsCollection = new ObservableCollection<BaseTreeViewModel>(
                (from child in this._folderModel.Folders
                 select new FolderTreeViewModel(child, this, mainWindowViewModel))
                .ToList());

            foreach (LoginModel wm in this._folderModel.Logins)
            {
                _folderViewModelsCollection.Add(new LoginTreeViewModel(wm, this, mainWindowViewModel));
            }
        }

        public FolderTreeViewModel(FolderModel folderModel, FolderTreeViewModel parent, MainWindowViewModel mainWindowViewModel)
        {
            this._folderModel = folderModel;
            _mainWindowViewModelObserver = mainWindowViewModel;
            _parent = parent;
            _folderViewModelsCollection = new ObservableCollection<BaseTreeViewModel>(
                (from child in this._folderModel.Folders
                 select new FolderTreeViewModel(child, this, mainWindowViewModel))
                .ToList());

            foreach (LoginModel wm in this._folderModel.Logins)
            {
                _folderViewModelsCollection.Add(new LoginTreeViewModel(wm, this, mainWindowViewModel));
            }
        }

        #endregion

        #region Properties
        private FolderModel _folderModel;
        private MainWindowViewModel _mainWindowViewModelObserver;
        private FolderTreeViewModel _parent;

        private ObservableCollection<BaseTreeViewModel> _folderViewModelsCollection;
        public ObservableCollection<BaseTreeViewModel> FolderViewModelsCollection
        {
            get
            {
                return _folderViewModelsCollection;
            }
            set
            {
                _folderViewModelsCollection = value;
                OnPropertyChanged("FolderViewModelsCollection");
            }
        }

        public string Name
        {
            get { return _folderModel.Name; }
            set
            {
                _folderModel.Name = value;
                OnPropertyChanged("Name");
            }
        }
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
                        _mainWindowViewModelObserver.ActiveFolderViewModel = this;
                    else
                        _mainWindowViewModelObserver.ActiveFolderViewModel = null;
                    OnPropertyChanged("IsSelected");
                }
            }
        }
        #endregion

        #region Methods
        public void AddFolder(FolderModel folderModel, MainWindowViewModel mainWindowViewModel)
        {
            this._folderModel.Folders.Add(folderModel);
            FolderTreeViewModel newFolderViewModel = new FolderTreeViewModel(folderModel, this, mainWindowViewModel);
            _folderViewModelsCollection.Add(newFolderViewModel);
        }

        public void DeleteFolder()
        {
            if (_parent != null)
                _parent.DeleteFolder(this);
            else
                _mainWindowViewModelObserver.DeleteFolderTreeViewModelFromRoot(_folderModel, this);
        }

        public void DeleteFolder(FolderTreeViewModel folderTreeViewModel)
        {
            _folderModel.Folders.Remove(folderTreeViewModel._folderModel);
            _folderViewModelsCollection.Remove(folderTreeViewModel);
        }

        public void AddLogin(LoginModel loginModel)
        {
            _folderModel.Logins.Add(loginModel);
            LoginTreeViewModel newLoginViewModel = new LoginTreeViewModel(loginModel, this, _mainWindowViewModelObserver);
            FolderViewModelsCollection.Add(newLoginViewModel);
        }

        public void DeleteLogin(LoginModel loginModel, LoginTreeViewModel loginTreeViewModel)
        {
            _folderModel.Logins.Remove(loginModel);
            _folderViewModelsCollection.Remove(loginTreeViewModel);
        }
        #endregion
    }
}
