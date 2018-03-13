// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// Author: 
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace FriendOrganizer.UI
{
    using System.Windows;

    using MahApps.Metro.Controls;

    using ViewModel;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        #region Fields

        private readonly MainViewModel _viewModel;

        #endregion

        #region Constructors

        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            _viewModel = viewModel;
            Loaded += MainWindowLoaded;
        }

        #endregion

        #region Methods

        private async void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadAsync();
        }

        #endregion
    }
}
