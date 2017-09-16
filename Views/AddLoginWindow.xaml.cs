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
using System.Windows.Shapes;
using PasswordKeeper.DataModel;
using PasswordKeeper.ViewModels;

namespace PasswordKeeper.Views
{
    /// <summary>
    /// Interaction logic for AddLoginWindow.xaml
    /// </summary>
    public partial class AddLoginWindow : Window
    {
        internal LoginModel LoginModel { get; private set; }

        public AddLoginWindow()
        {
            InitializeComponent();
            LoginModel = new LoginModel();
            LoginCreationViewModel loginCreationViewModel = new LoginCreationViewModel(LoginModel);
            DataContext = loginCreationViewModel;
        }

        private void button_CANCEL_Click(object sender, RoutedEventArgs e)
        {
            this.LoginModel = null;
            Close();
        }

        private void button_OK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //Put password into temporal container
        private void passwordBox_Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext != null)
                ((dynamic)DataContext).TemporalPasswordContainer = ((PasswordBox)sender).Password;
        }
    }
}
