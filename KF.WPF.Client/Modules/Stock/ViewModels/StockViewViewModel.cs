using KF.WPF.Client.Core.APIClient.RestServices;
using KF.WPF.Client.Core.Models;
using Prism;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace KF.WPF.Client.Modules.Stock.ViewModels
{
    public class StockViewViewModel : BindableBase, IActiveAware
    {
        #region Properties
        private readonly StockRestService stockRestService;
        private readonly ProductRestService productRestService;
        private string searchString;

        public string SearchString
        {
            get { return searchString; }
            set { SetProperty(ref searchString, value); }
        }

        private List<StockModel> allStocks;

        private ObservableCollection<StockModel> stocks;
        public ObservableCollection<StockModel> Stocks
        {
            get { return stocks; }
            set { SetProperty(ref stocks, value); }
        }

        private ObservableCollection<ProductModel> products;
        public ObservableCollection<ProductModel> Products
        {
            get { return products; }
            set { SetProperty(ref products, value); }
        }

        private StockModel selectedStock;
        public StockModel SelectedStock
        {
            get { return selectedStock; }
            set { SetProperty(ref selectedStock, value); }
        }


        #endregion

        #region ctor

        public StockViewViewModel(StockRestService stockRestService, ProductRestService productRestService)
        {
            this.stockRestService = stockRestService;
            this.productRestService = productRestService;
            AddStockCommand = new DelegateCommand(AddStock);
            UpdateStockCommand = new DelegateCommand(UpdateStock);
            DeleteStockCommand = new DelegateCommand(DeleteStock);
            Task.Run(() => this.Initialize()).Wait();
        }

        #endregion

        #region Methods

        private async Task Initialize()
        {
            await GetStocks();
        }

        private async Task GetStocks()
        {
            Stocks = new ObservableCollection<StockModel>(await stockRestService.GetAllStocksAsync());
            allStocks = new List<StockModel>(Stocks.AsEnumerable());
            //Products = new ObservableCollection<ProductModel>(await productRestService.GetAllProductsAsync());
        }

        #endregion

        #region Commands

        bool active;
        public bool IsActive
        {
            get
            {
                return active;
            }
            set
            {
                if (active != value)
                {
                    active = value;
                    if (active == true)
                        Task.Run(async() => await GetStocks());
                }
            }
        }

        private DelegateCommand _searchStringCommand;

        public event EventHandler IsActiveChanged;

        public DelegateCommand SearchStringCommand =>
            _searchStringCommand ?? (_searchStringCommand = new DelegateCommand(ExecuteSearchString, CanExecuteSearchString)).ObservesProperty(() => SearchString);

        async void ExecuteSearchString()
        {
            var searchStock = allStocks.Where(x => x.ProductName.ToLower().StartsWith(SearchString.ToLower()));
            Stocks.Clear();
            Stocks.AddRange(searchStock);
        }

        bool CanExecuteSearchString()
        {
            return true;
        }

        public DelegateCommand AddStockCommand { get; private set; }

        private async void AddStock()
        {
            try
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
            catch(Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public DelegateCommand UpdateStockCommand { get; private set; }

        private async void UpdateStock()
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public DelegateCommand DeleteStockCommand { get; private set; }

        private async void DeleteStock()
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        #endregion
    }
}
