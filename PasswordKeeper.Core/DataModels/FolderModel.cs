using System;
using System.Collections.Generic;

namespace PasswordKeeper.Core
{
    [Serializable]
    public class FolderModel
    {
        #region Properties
        readonly List<FolderModel> _folders;
        public List<FolderModel> Folders
        {
            get { return _folders; }
        }

        readonly List<CredentialsModel> _logins;
        public List<CredentialsModel> Logins
        {
            get { return _logins; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        #endregion

        public FolderModel(string name)
        {
            Name = name;
            _folders = new List<FolderModel>();
            _logins = new List<CredentialsModel>();
        }        
    }
}
