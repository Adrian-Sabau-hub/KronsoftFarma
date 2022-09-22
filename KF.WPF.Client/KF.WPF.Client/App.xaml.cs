using KF.WPF.Client.Core.APIClient;
using KF.WPF.Client.Core.APIClient.RestServices;
using KF.WPF.Client.Modules.Product.ViewModels;
using KF.WPF.Client.Modules.Product.Views;
using KF.WPF.Client.Modules.Stock.ViewModels;
using KF.WPF.Client.Modules.Stock.Views;
using KF.WPF.Client.Modules.User.ViewModels;
using KF.WPF.Client.Modules.User.Views;
using KF.WPF.Client.Services;
using KF.WPF.Client.Services.Interfaces;
using KF.WPF.Client.ViewModels;
using KF.WPF.Client.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Product;
using Stock;
using System.Windows;
using Toolbar;
using User;

namespace KF.WPF.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //wpf-prism-7-login
            containerRegistry.Register(typeof(object), typeof(Login), "Login");
            ViewModelLocationProvider.Register<Login, LoginViewModel>();

            //containerRegistry.RegisterInstance(typeof(LoginViewModel), new LoginViewModel(Container.GetContainer(), Container.Resolve<RegionManager>()));
            containerRegistry.Register(typeof(object), typeof(MainWindow), "MainWindow");
            //containerRegistry.RegisterInstance(typeof(MainWindowViewModel), new MainWindowViewModel(Container.GetContainer(), Container.Resolve<RegionManager>()));


            containerRegistry.RegisterSingleton<ProductRestService>();
            ViewModelLocationProvider.Register<ProductView, ProductViewViewModel>();

            containerRegistry.RegisterSingleton<UserRestService>();
            ViewModelLocationProvider.Register<UserView, UserViewViewModel>();

            containerRegistry.RegisterSingleton<StockRestService>();
            ViewModelLocationProvider.Register<StockView, StockViewViewModel>();

            containerRegistry.Register<IHttpClientFactory, HttpClientFactory>();
            containerRegistry.Register<IClientApplicationConfiguration, ApplicationConfiguration>();
            containerRegistry.RegisterSingleton<IUserDataService, UserDataService>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ToolbarModule>();
            moduleCatalog.AddModule<ProductModule>();
            moduleCatalog.AddModule<StockModule>();
            moduleCatalog.AddModule<UserModule>();
        }

        protected override void OnInitialized()
        {
            var login = Container.Resolve<Login>();
            var result = login.ShowDialog();

            if(result.Value)
                base.OnInitialized();

            else Application.Current.Shutdown();
        }
    }
}
