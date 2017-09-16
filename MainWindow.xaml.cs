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
using PasswordKeeper.ViewModels;
using PasswordKeeper.DataModel;
using PasswordKeeper.Views;
using System.IO;

namespace PasswordKeeper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _mainWindowViewModel;

        public MainWindow()
        {
            InitializeComponent();

            if (Database.Exists)
            {
                LoginToDatabase();
            }
            else
            {
                CreateNewDatabase();
            }

            DataContext = _mainWindowViewModel;
        }

        private void LoginToDatabase()
        {
            Database database = DatabaseSerializer.LoadData();
            GetPasswordWindow getPasswordWindow = new GetPasswordWindow();
            getPasswordWindow.ShowDialog();
            if (!getPasswordWindow.Valid)
            {
                Close();
                return;
            }
            try
            {
                database.CheckPassword(getPasswordWindow.Password);
                _mainWindowViewModel = new MainWindowViewModel(this, database);
            }
            catch (UnauthorizedAccessException)
            {                
                MessageBox.Show("Wrong password");
                Close();
            }
        }

        private void CreateNewDatabase()
        {
            InitialPassAndKeywordWindow initWindow = new InitialPassAndKeywordWindow();
            initWindow.ShowDialog();
            if (!initWindow.initialPasswordViewModel.Valid)
            {
                Close();
                return;
            }
            Database database = new Database(initWindow.initialPasswordViewModel.Keyword, initWindow.initialPasswordViewModel.Password);
            _mainWindowViewModel = new MainWindowViewModel(this, database);
            _mainWindowViewModel.SaveDataMethod();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_mainWindowViewModel == null)
                return;
            if (_mainWindowViewModel.UnsavedChanges)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Save changes to the vault?", "Save chages?", MessageBoxButton.YesNoCancel);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    _mainWindowViewModel.SaveDataMethod();
                    e.Cancel = false;
                }
                if (messageBoxResult == MessageBoxResult.No)
                    e.Cancel = false;
                if (messageBoxResult == MessageBoxResult.Cancel)
                    e.Cancel = true;
            }
        }

        private void MenuItemClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
