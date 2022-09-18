using KF.WPF.Client.Core;
using KF.WPF.Client.Modules.User.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace User
{
    public class UserModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public UserModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            //_regionManager.RequestNavigate(RegionNames.ContentRegion, "UserView");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<UserView>();
        }
    }
}