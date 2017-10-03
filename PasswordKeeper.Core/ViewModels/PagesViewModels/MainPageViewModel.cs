using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PasswordKeeper.Core
{
    public class MainPageViewModel : BaseViewModel
    {
        public SideMenuControlViewModel SideMenuControlViewModel { get; set; }

        public MainPageViewModel()
        {
            SideMenuControlViewModel = new SideMenuControlViewModel();            
        }
    }
}
