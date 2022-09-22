using KF.WPF.Client.Modules.Stock.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Stock
{
    public class StockModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public StockModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<StockView>();
        }
    }
}