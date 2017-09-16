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

namespace PasswordKeeper.Views
{
    /// <summary>
    /// Interaction logic for GetMainPasswordWindow.xaml
    /// </summary>
    public partial class GetPasswordWindow : Window
    {
        public bool Valid { get; private set; }
        public string Password { get; private set; }

        public GetPasswordWindow()
        {
            InitializeComponent();
        }

        private void passwordBox_MainPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Password = ((PasswordBox)sender).Password;
        }

        private void button_OK_Click(object sender, RoutedEventArgs e)
        {
            Valid = true;
            Close();
        }

        private void button_CANCEL_Click(object sender, RoutedEventArgs e)
        {
            Valid = false;
            Close();
        }
    }
}
