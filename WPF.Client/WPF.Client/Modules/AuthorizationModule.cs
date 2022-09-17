using KF.Services.Login;
using KF.WPF.Client.APIClient;
using Prism.Ioc;
using Prism.Modularity;
using WPF.Client.APIClient.RestServices;
using WPF.Client.Views;

namespace KF.WPF.Client.Modules
{
    public class AuthorizationModule : IModule
    {
        //wpf-prism-7-login - all class

        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register(typeof(ILoginService), typeof(LoginService));
        }
    }
}
