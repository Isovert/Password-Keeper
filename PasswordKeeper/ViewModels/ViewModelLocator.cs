using PasswordKeeper.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordKeeper
{
    public class ViewModelLocator
    {

        //private static ViewModelLocator _viewModelLocator;
        //public static ViewModelLocator Instance
        //{
        //    get
        //    {
        //        if (_viewModelLocator == null)
        //        {
        //            _viewModelLocator = new ViewModelLocator();
        //            return _viewModelLocator;
        //        }
        //        else
        //            return _viewModelLocator;
        //    }
        //}

        //public static ApplicationViewModel ApplicationViewModel
        //{
        //    get
        //    {
        //        return IoC.Get<ApplicationViewModel>();
        //    }

        public static ViewModelLocator Instance { get; private set; } = new ViewModelLocator();

        public static ApplicationViewModel ApplicationViewModel { get; } = IoC.Get<ApplicationViewModel>();


    }
}

