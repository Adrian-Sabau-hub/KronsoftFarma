using KF.WPF.Client.Modules.Product.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Product
{
    public class ProductModule : IModule
    {

        private readonly IRegionManager _regionManager;

        public ProductModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ProductView>();
        }
        
    }
}