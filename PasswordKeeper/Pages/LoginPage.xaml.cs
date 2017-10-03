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
using System.Security;

namespace PasswordKeeper
{
    /// <summary>
    /// Interaction logic for InitializationPage.xaml
    /// </summary>
    public partial class LoginPage : BasePage<LoginViewModel>
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        //Dirty but working
        private void MainPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ViewModel.SetPassword(((PasswordBox)sender).SecurePassword);
        }        
    }
}
