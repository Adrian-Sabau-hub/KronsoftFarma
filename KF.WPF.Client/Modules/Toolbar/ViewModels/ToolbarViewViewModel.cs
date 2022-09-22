using KF.WPF.Client.Core;
using KF.WPF.Client.Services.Interfaces;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using Unity;

namespace KF.WPF.Client.Modules.Toolbar.ViewModels
{
    public class ToolbarViewViewModel : BindableBase
    {
        #region Fields

        private readonly IRegionManager regionManager;
        private readonly IUnityContainer containerRegistry;
        private readonly IUserDataService userDataService;

        #endregion

        #region Properties

        private bool showUser;
        public bool ShowUser
        {
            get { return showUser; }
            set { SetProperty(ref showUser, value); }
        }

        #endregion

        #region ctor

        public ToolbarViewViewModel(IRegionManager regionManager, IUnityContainer container, IUserDataService userDataService)
        {
            this.regionManager = regionManager;
            this.containerRegistry = container;
            this.userDataService = userDataService;
        }

        #endregion

        #region Methods

        public void UpdateUserButtonVisibility()
        {
            ShowUser = userDataService.IsAdmin;
        }

        #endregion

        #region Commands

        private DelegateCommand<string> _navigate;
        public DelegateCommand<string> Navigate =>
            _navigate ?? (_navigate = new DelegateCommand<string>(ExecuteNavigate));

        void ExecuteNavigate(string screenUri)
        {
            // if there is no MainRegion then something is very wrong
            if (this.regionManager.Regions.ContainsRegionWithName(RegionNames.ContentRegion))
            {
                // see if this view is already loaded into the region
                var view = this.regionManager.Regions[RegionNames.ContentRegion].GetView(screenUri);
                if (view == null)
                {
                    // if not then load it now
                    switch (screenUri)
                    {
                        case "ProductView":
                            this.regionManager.Regions[RegionNames.ContentRegion].Add(containerRegistry.Resolve<Modules.Product.Views.ProductView>(), screenUri);
                            break;
                        case "StockView":
                            this.regionManager.Regions[RegionNames.ContentRegion].Add(containerRegistry.Resolve<Modules.Stock.Views.StockView>(), screenUri);
                            break;
                        case "UserView":
                            this.regionManager.Regions[RegionNames.ContentRegion].Add(containerRegistry.Resolve<Modules.User.Views.UserView>(), screenUri);
                            break;
                        default:
                            throw new Exception(string.Format("Unknown screenUri: {0}", screenUri));
                    }
                    // and retrieve it into our view variable
                    view = this.regionManager.Regions[RegionNames.ContentRegion].GetView(screenUri);
                }
                // make the view the active view
                this.regionManager.Regions[RegionNames.ContentRegion].Activate(view);
            }
        }

        #endregion
    }
}
