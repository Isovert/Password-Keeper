using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PasswordKeeper.Core;

namespace PasswordKeeper
{
    /// <summary>
    /// Interaction logic for InitializationPage.xaml
    /// </summary>
    public partial class InitializationPage : BasePage<InitializationViewModel>
    {
        public InitializationPage()
        {
            InitializeComponent();
        }

        //Dirty but working
        private void MainPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ViewModel.SetAndCheckPassword(((PasswordBox)sender).SecurePassword);
        }

        //Dirty but working
        private void ConfirmPasswordText_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ViewModel.SetRetypedPassword(((PasswordBox)sender).SecurePassword);
        }
    }
}
