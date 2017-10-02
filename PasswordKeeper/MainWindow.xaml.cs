using System;
using System.Threading;
using System.Windows;

namespace PasswordKeeper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {   
        
        public MainWindow()
        {
            InitializeComponent();
             
            DataContext = new MainWindowViewModel(this);
        }       
    }
}
