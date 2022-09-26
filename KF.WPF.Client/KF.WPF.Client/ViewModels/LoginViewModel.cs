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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using Unity;

namespace KF.WPF.Client.ViewModels
{
    public class LoginViewModel : BindableBase,INotifyPropertyChanged
    {
        #region Properties
        private readonly UserRestService userRestService;

        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;
        private readonly IUserDataService userDataService;

        //public ObservableCollection<LoginViewModel> CanExecuteValidateUser;

        private string username;// = "Adrian";
        public string Username
        {
            get { return username; }
            set 
            { 
                SetProperty(ref username, value);
            }
        }

        private string password;// = "1234567890";
        public string Password
        {
            get { return password; }
            set
            {
                SetProperty(ref password, value);
            }
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

        #endregion

        #region ctor

        public LoginViewModel(IUnityContainer container, IRegionManager regionManager, UserRestService userRestService, IUserDataService userDataService)
        {
            this.userRestService = userRestService;
            this.userDataService = userDataService;
            _regionManager = regionManager;
            _container = container;
        }
        #endregion

        #region Commands

        private DelegateCommand<Window> _validateUser;
        public DelegateCommand<Window> ValidateUser =>
            _validateUser ?? (_validateUser = new DelegateCommand<Window>(ExecuteValidateUser, CanExecuteValidateUser)).ObservesProperty(() => Username).ObservesProperty(() => Password);

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
                //window.DialogResult = false;
                MessageBox.Show("Wrong username or password");
                return;
            }
        }

        bool CanExecuteValidateUser(Window window)
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);// && Username.Length >= 3 && Password.Length >= 3;
        }

        #endregion
    }
}
