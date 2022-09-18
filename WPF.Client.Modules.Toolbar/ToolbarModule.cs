using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using WPF.Client.Core;
using WPF.Client.Modules.Toolbar.Views;

namespace WPF.Client.Modules.Toolbar
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
            _regionManager.RequestNavigate(RegionNames.ToolbarRegion, "PrismUserControl1");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Toolbar.Views.Toolbar,Toolbar.ViewModels.ToolbarViewViewModel>();
            containerRegistry.RegisterForNavigation<Toolbar.Views.PrismUserControl1, Toolbar.ViewModels.PrismUserControl1ViewModel>();
        }
    }
}