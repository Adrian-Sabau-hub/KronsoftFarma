using KF.WPF.Client.Core;
using KF.WPF.Client.Core.APIClient.RestServices;
using KF.WPF.Client.Services.Interfaces;
using KF.WPF.Client.Views;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Unity;
using System;
using System.Windows;
using Unity;

namespace KF.WPF.Client.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        #region Properties
        private readonly UserRestService userRestService;

        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;
        private readonly IUserDataService userDataService;

        public LoginViewModel(IUnityContainer container, IRegionManager regionManager, UserRestService userRestService, IUserDataService userDataService)
        {
            this.userRestService = userRestService;
            this.userDataService = userDataService;
            _regionManager = regionManager;
            _container = container;
        }


        private string username;// = "Adrian";
        public string Username
        {
            get { return username; }
            set { SetProperty(ref username, value); }
        }


        private string password;// = "1234567890";
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }

        #endregion



        private DelegateCommand<Window> _validateUser;
        public DelegateCommand<Window> ValidateUser =>
            _validateUser ?? (_validateUser = new DelegateCommand<Window>(ExecuteValidateUser, CanExecuteValidateUser));


        async void ExecuteValidateUser(Window window)
        {
             var result = await userRestService.ValidateUser(username, password);
            if(result != null)
            {
                userDataService.Username = Username;
                userDataService.Password = Password;
                userDataService.IsAdmin = result.IsAdmin;

                _regionManager.Regions[RegionNames.ContentRegion].Add(_container.Resolve<Modules.Product.Views.ProductView>(), "ProductView");
                _regionManager.Regions[RegionNames.ToolbarRegion].Add(_container.Resolve<Modules.Toolbar.Views.Toolbar>(), "Toolbar");

                window.DialogResult = true;

            } else
            {
                window.DialogResult = false;
                MessageBox.Show("Wrong username or password");
                return;
            }
        }

        bool CanExecuteValidateUser(Window window)
        {
            return true;
        }

        //wpf-prism-7-login



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

    }
}
