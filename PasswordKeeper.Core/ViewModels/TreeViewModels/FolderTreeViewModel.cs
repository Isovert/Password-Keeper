using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace PasswordKeeper.Core
{
    public class FolderTreeViewModel : BaseTreeViewModel
    {
        #region Constructors
        public FolderTreeViewModel(FolderModel folderModel)
        {
            _folderModel = folderModel;
            _folderViewModelsCollection = new ObservableCollection<BaseTreeViewModel>(
                (from child in this._folderModel.Folders
                 select new FolderTreeViewModel(child, this))
                .ToList());
            
        }

        public FolderTreeViewModel(FolderModel folderModel, FolderTreeViewModel parent)
        {
            _folderModel = folderModel;
            _parent = parent;
            _folderViewModelsCollection = new ObservableCollection<BaseTreeViewModel>(
                (from child in _folderModel.Folders
                 select new FolderTreeViewModel(child, this))
                .ToList());
            
        }

        #endregion

        #region Properties
        private FolderModel _folderModel;
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
                    OnPropertyChanged("IsSelected");
                }
            }
        }
        #endregion

        #region Methods
        public void AddFolder(FolderModel folderModel)
        {
            this._folderModel.Folders.Add(folderModel);
            FolderTreeViewModel newFolderViewModel = new FolderTreeViewModel(folderModel, this);
            _folderViewModelsCollection.Add(newFolderViewModel);
        }

        public void DeleteFolder()
        {
            throw new NotImplementedException();
        }

        public void DeleteFolder(FolderTreeViewModel folderTreeViewModel)
        {
            _folderModel.Folders.Remove(folderTreeViewModel._folderModel);
            _folderViewModelsCollection.Remove(folderTreeViewModel);
        }

        public void AddLogin(LoginModel loginModel)
        {
            _folderModel.Logins.Add(loginModel);
            LoginTreeViewModel newLoginViewModel = new LoginTreeViewModel(loginModel, this);
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
