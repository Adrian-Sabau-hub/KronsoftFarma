using KF.WPF.Client.APIClient;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Unity;
using System.Windows;
using WPF.Client.APIClient.RestServices;
using WPF.Client.Modules.ModuleName;
using WPF.Client.Modules.Toolbar.Views;
using WPF.Client.Services;
using WPF.Client.Services.Interfaces;
using WPF.Client.ViewModels;
using WPF.Client.Views;

namespace WPF.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<Login>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //wpf-prism-7-login
            containerRegistry.Register(typeof(object), typeof(Login), "Login");
            ViewModelLocationProvider.Register<Login, LoginViewModel>();

            //containerRegistry.RegisterInstance(typeof(LoginViewModel), new LoginViewModel(Container.GetContainer(), Container.Resolve<RegionManager>()));
            containerRegistry.Register(typeof(object), typeof(MainWindow), "MainWindow");
            containerRegistry.RegisterInstance(typeof(MainWindowViewModel), new MainWindowViewModel(Container.GetContainer(), Container.Resolve<RegionManager>()));


            containerRegistry.RegisterSingleton<ProductRestService>();
            ViewModelLocationProvider.Register<ProductView, ProductViewViewModel>();

            containerRegistry.RegisterSingleton<UserRestService>();
            ViewModelLocationProvider.Register<UserView, UserViewViewModel>();

            containerRegistry.RegisterSingleton<StockRestService>();
            ViewModelLocationProvider.Register<StockView, StockViewViewModel>();

            containerRegistry.Register<IHttpClientFactory, HttpClientFactory>();
            containerRegistry.Register<IClientApplicationConfiguration, ApplicationConfiguration>();
            containerRegistry.RegisterSingleton<IMessageService, MessageService>();

            

        }

        //wpf-prism-7-login
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ModuleNameModule>();
            moduleCatalog.AddModule<Modules.Toolbar.ToolbarModule>();
        }
    }
}
