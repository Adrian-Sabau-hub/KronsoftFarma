using KF.WPF.Client.APIClient;
using Prism.Ioc;
using Prism.Modularity;
using WPF.Client.APIClient.RestServices;
using WPF.Client.Views;

namespace KF.WPF.Client.Modules
{
    public class MainModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Login>();
            containerRegistry.RegisterForNavigation<ProductView>();
            containerRegistry.RegisterForNavigation<StockView>();
            containerRegistry.RegisterForNavigation<UserView>();
            containerRegistry.Register<IHttpClientFactory, HttpClientFactory>();
            containerRegistry.Register<IClientApplicationConfiguration, ApplicationConfiguration>();
            containerRegistry.Register<ProductRestService>();
            containerRegistry.Register<StockRestService>();
            containerRegistry.Register<UserRestService>();

        }
    }
}
