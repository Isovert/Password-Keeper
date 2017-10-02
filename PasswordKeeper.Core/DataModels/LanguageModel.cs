namespace PasswordKeeper.Core
{
    internal class LanguageModel
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            private set { _name = value; }
        }

        private string _attribute;
        public string Attribute
        {
            get { return _attribute; }
            private set { _attribute = value; }
        }
        
        internal LanguageModel(string name, string attribute)
        {
            _name = name;
            _attribute = attribute;
        }
    }
}
