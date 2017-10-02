using PasswordKeeper.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PasswordKeeper
{
    public class BasePage<VM> : Page
            where VM : BaseViewModel, new()
    {        
        private VM mViewModel;
        public VM ViewModel
        {
            get => mViewModel;
            set
            {
                // If nothing has changed, return
                if (mViewModel == value)
                    return;

                // Update the value
                mViewModel = value;

                // Set the data context for this page
                DataContext = mViewModel;
            }
        }
        
        public BasePage()
        {
            ViewModel = new VM();
        }                
    }
}
