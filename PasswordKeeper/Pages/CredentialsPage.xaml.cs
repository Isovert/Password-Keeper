using PasswordKeeper.Core;
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

namespace PasswordKeeper
{
    /// <summary>
    /// Interaction logic for CredentialsPage.xaml
    /// </summary>
    public partial class CredentialsPage : BasePage<CredentialsViewModel>
    {
        public CredentialsPage()
        {
            InitializeComponent();
        }

        public void passwordBox_Password_PasswordChanged(object sender, EventArgs e)
        {

        }
    }
}
