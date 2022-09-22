using KF.WPF.Client.Modules.Toolbar.ViewModels;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace KF.WPF.Client.Modules.Toolbar.Views
{
    /// <summary>
    /// Interaction logic for ViewA.xaml
    /// </summary>
    public partial class Toolbar : UserControl
    {
        public Toolbar()
        {
            InitializeComponent();
            this.Loaded += Toolbar_Loaded;
        }

        private void Toolbar_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            ((sender as Toolbar).DataContext as ToolbarViewViewModel).UpdateUserButtonVisibility();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.MainWindow.Close();
        }

    }
}
