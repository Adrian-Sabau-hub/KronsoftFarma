using System.Windows;
using System.Windows.Controls;

namespace KF.WPF.Client.Modules.Product.Views
{
    /// <summary>
    /// Interaction logic for Product.xaml
    /// </summary>
    public partial class ProductView : UserControl
    {
        public ProductView()
        {
            InitializeComponent();
        }

        void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
    }
}
