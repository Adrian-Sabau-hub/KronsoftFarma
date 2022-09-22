using System.Windows;
using System.Windows.Controls;

namespace KF.WPF.Client.Modules.Stock.Views
{
    /// <summary>
    /// Interaction logic for Product.xaml
    /// </summary>
    public partial class StockView : UserControl
    {
        public StockView()
        {
            InitializeComponent();
        }

        void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
    }
}
