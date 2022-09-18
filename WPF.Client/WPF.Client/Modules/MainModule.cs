using KF.WPF.Client.APIClient;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using WPF.Client.APIClient.RestServices;
using WPF.Client.Core;
using WPF.Client.Modules.Toolbar.Views;
using WPF.Client.Views;

namespace KF.WPF.Client.Modules
{
    public class MainModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public MainModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            //_regionManager.RequestNavigate(RegionNames.ToolbarRegion, "Toolbar");
            _regionManager.RequestNavigate(RegionNames.ToolbarRegion, "PrismUserControl1");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Login>();
            containerRegistry.RegisterForNavigation<Toolbar>();
            containerRegistry.RegisterForNavigation<PrismUserControl1>();
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
