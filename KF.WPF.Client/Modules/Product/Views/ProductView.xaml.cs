using KF.WPF.Client.Modules.Product.ViewModels;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

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
