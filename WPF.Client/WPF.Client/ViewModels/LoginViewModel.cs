using KF.Services.User;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unity;
using WPF.Client.APIClient.RestServices;
using WPF.Client.Services.Interfaces;
using WPF.Client.Views;

namespace WPF.Client.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        #region Properties
        private readonly UserRestService userRestService;

        private string username = "Adrian";
        public string Username
        {
            get { return username; }
            set { SetProperty(ref username, value); }
        }
        

        private string password = "1234567890";
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }

        #endregion

        public LoginViewModel(UserRestService userRestService)
        {
            this.userRestService = userRestService;
        }

        private DelegateCommand _validateUser;
        public DelegateCommand ValidateUser =>
            _validateUser ?? (_validateUser = new DelegateCommand(ExecuteValidateUser, CanExecuteValidateUser));

        async void ExecuteValidateUser()
        {
             var result = await userRestService.ValidateUser(username, password);
            if(result == true)
            {
                App.Current.Properties["username"] = username;
                App.Current.Properties["password"] = password;

                NewWindow.Invoke(_container.Resolve<MainWindow>());
                CloseWindow.Invoke();
            } else
            {
                MessageBox.Show("Wrong username or password");
                return;
            }
        }

        bool CanExecuteValidateUser()
        {
            return true;
        }

        //wpf-prism-7-login

        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;
        private PrismApplication _application;


        private string _title = "Kronsoft Pharma";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public DelegateCommand LoginCommand { get; set; }
        public delegate void NewWindowDelegate(Object win);
        public delegate void CloseWindowDelegate();
        public CloseWindowDelegate CloseWindow { get; set; }
        public NewWindowDelegate NewWindow { get; set; }

        public LoginViewModel(IUnityContainer container, IRegionManager regionManager, UserRestService userRestService)
        {
            this.userRestService = userRestService;
            _regionManager = regionManager;
            _container = container;

            //LoginCommand = new DelegateCommand(OnLogin);
        }

        //private void OnLogin()
        //{
        //    Trace.WriteLine("Logging in");
        //    // do your login stuff

        //    // If Login OK continue here
        //    NewWindow.Invoke(_container.Resolve<MainWindow>());
        //    CloseWindow.Invoke();

        //}


    }
}
