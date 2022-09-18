using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using WPF.Client.Core;
using WPF.Client.Modules.ModuleName.Views;
using WPF.Client.Modules.Toolbar.Views;

namespace WPF.Client.Modules.ModuleName
{
    public class ModuleNameModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public ModuleNameModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RequestNavigate(RegionNames.ToolbarRegion, "Toolbar");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<PrismUserControl1>();
        }
    }
}