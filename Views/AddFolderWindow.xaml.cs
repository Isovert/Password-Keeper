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

namespace PasswordKeeper.Views
{
    /// <summary>
    /// Interaction logic for AddFolderWindow.xaml
    /// </summary>
    public partial class AddFolderWindow : Window
    {
        public bool Valid { get; private set; }
        internal FolderModel FolderModel { get; private set; }
            
        public AddFolderWindow()
        {
            InitializeComponent();
            FolderModel = null;
            Valid = false;
        }

        private void button_OK_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(textBox_FolderName.Text))
            {
                return;
            }
            Valid = true;
            FolderModel = new FolderModel(textBox_FolderName.Text);
            Close();
        }

        private void button_CANCEL_Click(object sender, RoutedEventArgs e)
        {
            Valid = false;
            Close();
        }
    }
}
