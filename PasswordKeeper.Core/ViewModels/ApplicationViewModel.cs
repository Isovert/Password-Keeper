using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace PasswordKeeper.Core
{
    public class ApplicationViewModel : BaseViewModel
    {
        #region Private Members
        private Vault _vault;
        #endregion


        public ApplicationPage CurrentPage { get; private set; }

        public bool SideMenuVisible { get; set; }
        
        #region Public methods
        public ApplicationViewModel()
        {
            if (!Vault.Exists)
                CurrentPage = ApplicationPage.InitializationPage;
            else
            {
                _vault = VaultSerializer.LoadData();
                CurrentPage = ApplicationPage.LoginPage;
            }
        }

        public Vault GetVault()
        {
            return _vault;
        }

        public void GoToPage(ApplicationPage page)
        {
            CurrentPage = page;
        } 

        public void CreateNewVault(SecureString password)
        {
            _vault = new Vault(password);
            VaultSerializer.SaveData(_vault);
        }

        public void LoadVault(SecureString password)
        {
            _vault.TryLogIn(password);
        }
        #endregion

    }
}
