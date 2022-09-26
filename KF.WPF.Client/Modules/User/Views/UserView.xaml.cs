using System;
using System.Windows.Controls;

namespace KF.WPF.Client.Modules.User.Views
{
    /// <summary>
    /// Interaction logic for Product.xaml
    /// </summary>
    public partial class UserView : UserControl
    {
        public UserView()
        {
            InitializeComponent();
            //System.Windows.Forms.NotifyIcon NotifyIcon = new System.Windows.Forms.NotifyIcon();
        }

        void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        //public void Users_CellFormatting(object sender, System.Windows.Forms.DataGridViewCellFormattingEventArgs e)
        //{
        //    if (e.ColumnIndex == 2 && e.Value != null)
        //    {
        //        e.Value = new String('*', e.Value.ToString().Length);
        //    }
        //}
    }
}
