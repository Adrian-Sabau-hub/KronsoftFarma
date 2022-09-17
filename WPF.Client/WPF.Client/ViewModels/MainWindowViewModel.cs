using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Unity;
using Unity;

namespace WPF.Client.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private readonly IRegionManager _regionManager;
        public DelegateCommand<string> NavigateCommand { get; private set; }


        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate("ContentRegion", navigatePath);
        }

        //wpf-prism-7-login
        private readonly IUnityContainer _container;
        //private readonly IRegionManager _regionManager;
        private PrismApplication _application;
        //private string _title = "Prism Application";
        //public string Title
        //{
        //    get { return _title; }
        //    set { SetProperty(ref _title, value); }
        //}

        public MainWindowViewModel(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }
    }
}
