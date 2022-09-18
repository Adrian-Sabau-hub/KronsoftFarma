using Prism.Mvvm;

namespace KF.WPF.Client.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Kronsoft Pharma";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {

        }
    }
}
