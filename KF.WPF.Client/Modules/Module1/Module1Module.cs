using KF.WPF.Client.Core;
using Module1.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Module1
{
    public class Module1Module : IModule
    {
        private readonly IRegionManager _regionManager;

        public Module1Module(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RequestNavigate(RegionNames.ToolbarRegion, "PrismUserControl1");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<PrismUserControl1>();
        }

       
    }
}