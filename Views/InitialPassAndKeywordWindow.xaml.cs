using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PasswordKeeper.ViewModels;

namespace PasswordKeeper.Views
{
    /// <summary>
    /// Interaction logic for InitialPassAndKeywordWindow.xaml
    /// </summary>
    public partial class InitialPassAndKeywordWindow : Window
    {
        internal readonly InitialPasswordViewModel initialPasswordViewModel;

        public InitialPassAndKeywordWindow()
        {
            InitializeComponent();
            initialPasswordViewModel = new InitialPasswordViewModel();
            DataContext = initialPasswordViewModel;
        }

        private void passwordBox_Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            initialPasswordViewModel.SetAndCheckPassword(((PasswordBox)sender).Password);
        }
        
        private void passwordBox_PasswordRetype_PasswordChanged(object sender, RoutedEventArgs e)
        {
            initialPasswordViewModel.SetRetypedPassword(((PasswordBox)sender).Password);
        }

        private void button_OK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void button_CANCEL_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
