using KF.WPF.Client.Core.APIClient.RestServices;
using KF.WPF.Client.Core.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace KF.WPF.Client.Modules.Stock.ViewModels
{
    public class StockViewViewModel : BindableBase
    {
        #region Properties
        private readonly StockRestService stockRestService;

        private ObservableCollection<StockModel> stocks;
        public ObservableCollection<StockModel> Stocks
        {
            get { return stocks; }
            set { SetProperty(ref stocks, value); }
        }

        private StockModel selectedStock;
        public StockModel SelectedStock
        {
            get { return selectedStock; }
            set { SetProperty(ref selectedStock, value); }
        }
        #endregion

        #region Commands

        public DelegateCommand AddStockCommand { get; private set; }

        private async void AddStock()
        {
            //get new stock
            StockModel newStock = Stocks.FirstOrDefault(s => s.StockId == Guid.Empty);

            if (newStock == null)
            {
                MessageBox.Show("Please enter a new stock in the grid.");
                return;
            }

            // send stock 
            await stockRestService.CreateStockAsync(newStock);

            //refresh lista
            await GetStocks();
        }

        public DelegateCommand UpdateStockCommand { get; private set; }

        private async void UpdateStock()
        {
            //check if selected stock
            if (SelectedStock == null)
            {
                MessageBox.Show("Please select a stock for update");
                return;
            }

            //get selected stock id
            Guid stockId = selectedStock.StockId;

            //send request to update 
            await stockRestService.UpdateStockAsync(stockId, SelectedStock);

            //refresh lista
            await GetStocks();
        }

        public DelegateCommand DeleteStockCommand { get; private set; }

        private async void DeleteStock()
        {
            //check if selected stock
            if (SelectedStock == null)
            {
                MessageBox.Show("Please select a stock");
                return;
            }

            //get selected stock id
            Guid stockId = selectedStock.StockId;

            //send request to delete Id via rest service
            await stockRestService.DeleteStockAsync(stockId);

            //refresh lista
            await GetStocks();
        }

        #endregion

        public StockViewViewModel(StockRestService stockRestService)
        {
            this.stockRestService = stockRestService;
            AddStockCommand = new DelegateCommand(AddStock);
            UpdateStockCommand = new DelegateCommand(UpdateStock);
            DeleteStockCommand = new DelegateCommand(DeleteStock);
            Task.Run(() => this.Initialize()).Wait();
        }

        private async Task Initialize()
        {
            await GetStocks();
        }

        private async Task GetStocks()
        {
            Stocks = new ObservableCollection<StockModel>(await stockRestService.GetAllStocksAsync());
        }

        

        



        


    }
}
