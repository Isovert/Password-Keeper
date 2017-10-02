using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordKeeper.Core
{
    public class ApplicationViewModel : BaseViewModel
    {
        //private ApplicationPage _currentPage;
        //public ApplicationPage CurrentPage
        //{
        //    get { return _currentPage; }
        //    private set
        //    {
        //        _currentPage = value;
        //        OnPropertyChanged(nameof(CurrentPage));
        //    }
        //}
                
        public ApplicationPage CurrentPage { get; private set; } = ApplicationPage.InitializationPage;
        
        //public ApplicationViewModel()
        //{
        //    CurrentPage = ApplicationPage.InitializationPage;
        //}

        public void GoToPage(ApplicationPage page)
        {
            CurrentPage = page;
        }
    }
}
