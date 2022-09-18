using KF.WPF.Client.Core;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Toolbar
{
    public class ToolbarModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public ToolbarModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RequestNavigate(RegionNames.ToolbarRegion, "Toolbar");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<KF.WPF.Client.Modules.Toolbar.Views.Toolbar, KF.WPF.Client.Modules.Toolbar.ViewModels.ToolbarViewViewModel>();
        }
    }
}