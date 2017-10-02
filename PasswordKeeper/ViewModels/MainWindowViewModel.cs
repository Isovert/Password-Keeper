using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using PasswordKeeper.Core;
using System.Windows;

namespace PasswordKeeper
{
    class MainWindowViewModel : BaseViewModel
    {
        #region Private Memebers

        Window _window;

        #endregion

        #region Public Properties

        public int MinWindowHeight { get; set; } = 800;
        public int MinWindowWidth { get; set; } = 600;
        public int TitleBarHeight { get; set; } = 42;

        #endregion

        public MainWindowViewModel(Window window)
        {
            _window = window;
            MinimizeCommand = new RelayCommand(x => _window.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(x => _window.WindowState = WindowState.Maximized);
            CloseCommand = new RelayCommand(x => _window.Close());
        }

        #region Commands

        public ICommand MinimizeCommand { get; set; }
        public ICommand MaximizeCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        #endregion

        #region Methods
        
        #endregion
    }
}
