using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PasswordKeeper.Core
{
    public class SideMenuControlViewModel : BaseViewModel
    {
        private ObservableCollection<FolderTreeViewModel> _firstGeneration;
        public ObservableCollection<FolderTreeViewModel> FirstGeneration
        {
            get { return _firstGeneration; }
            set { _firstGeneration = value; OnPropertyChanged(nameof(FirstGeneration)); }
        }

        public SideMenuControlViewModel()
        {
            CreateFolderCommand = new RelayCommand(x => CreateFolder());
            DeleteFolderCommand = new RelayCommand(x => DeleteFolder());
            CreateLoginCommand = new RelayCommand(x => CreateLogin());
            DeleteLoginCommand = new RelayCommand(x => DeleteLogin());
            Vault vault = IoC.Get<ApplicationViewModel>().GetVault();
            InitializeFirstGeneration(vault);
        }

        private void InitializeFirstGeneration(Vault vault)
        {
            FirstGeneration = new ObservableCollection<FolderTreeViewModel>();
            foreach (FolderModel fm in vault.RootFolderModels)
                FirstGeneration.Add(new FolderTreeViewModel(fm));
        }

        public ICommand CreateFolderCommand { get; set; }
        public ICommand DeleteFolderCommand { get; set; }
        public ICommand CreateLoginCommand { get; set; }
        public ICommand DeleteLoginCommand { get; set; }

        #region Methods
        private void CreateFolder()
        {
            IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.LoginPage);
            

        }

        private void DeleteFolder()
        {
            throw new NotImplementedException();
        }

        private void CreateLogin()
        {
            throw new NotImplementedException();
        }

        private void DeleteLogin()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
