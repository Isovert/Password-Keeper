using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordKeeper.Utillities;
using System.Diagnostics;
using System.Windows;

namespace PasswordKeeper.DataModel
{
    [Serializable]
    sealed class LoginModel
    {
        public LoginModel()
        {
            DateTime now = DateTime.Now;
            CreationDateTime = now;
            ModificationDateTime = now;
        }

        #region Properties
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _login;
        public string Login
        {
            get { return _login; }
            set { _login = value; }            
        }

        private string _notes;
        public string Notes
        {
            get { return _notes; }
            set { _notes = value; }
        }

        private string _url;
        public string URL
        {
            get { return _url; }
            set { _url = value; }
        }

        private byte[] _passwordEncrypted;
        private byte[] _initializationVector = new byte[16];

        private DateTime _creationDateTime;
        public DateTime CreationDateTime
        {
            get => _creationDateTime;
            private set => _creationDateTime = value;
        }

        private DateTime _modificationDateTime;
        public DateTime ModificationDateTime
        {
            get => _modificationDateTime;
            set => _modificationDateTime = value;
        }
        #endregion

        #region Methods
        private string GetPasswordString()
        {
            EncryptorAES encryptorAES = new EncryptorAES();
            return encryptorAES.DecryptStringFromBytes(_passwordEncrypted, Database.Key, _initializationVector);
        }
        
        public void CopyPasswordToClipboard()
        {
            Clipboard.SetText(GetPasswordString());
        }

        public void SavePassword(string password)
        {
            EncryptorAES encryptorAES = new EncryptorAES();
            var random = System.Security.Cryptography.RandomNumberGenerator.Create();
            random.GetBytes(_initializationVector);
            _passwordEncrypted = encryptorAES.EncryptStringToBytes(password, Database.Key, _initializationVector);
        }
        

        #endregion        
    }
}
