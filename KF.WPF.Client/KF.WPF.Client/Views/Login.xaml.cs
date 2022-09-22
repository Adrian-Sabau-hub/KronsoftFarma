using KF.WPF.Client.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace KF.WPF.Client.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            //wpf-prism-7-login
            ((LoginViewModel)DataContext).NewWindow += StartMainApp;
            ((LoginViewModel)DataContext).CloseWindow += CloseWindow;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password; }
        }

        //wpf-prism-7-login
        private void StartMainApp(Object win)
        {
            Application.Current.MainWindow = (Window)win;
            Application.Current.MainWindow.Show();
        }

        private void CloseWindow()
        {
            this.Close();
            //this.Hide();
        }
    }
}
