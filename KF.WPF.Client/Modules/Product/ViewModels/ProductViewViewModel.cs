using KF.WPF.Client.Core.APIClient.RestServices;
using KF.WPF.Client.Core.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace KF.WPF.Client.Modules.Product.ViewModels
{
    public class ProductViewViewModel : BindableBase
    {
        #region Properties
        private readonly ProductRestService productRestService;

        private ObservableCollection<ProductModel> products;
        public ObservableCollection<ProductModel> Products
        {
            get { return products; }
            set { SetProperty(ref products, value); }
        }

        private ProductModel selectedProduct;
        public ProductModel SelectedProduct
        {
            get { return selectedProduct; }
            set { SetProperty(ref selectedProduct, value); }
        }
        #endregion

        #region ctor

        public ProductViewViewModel(ProductRestService producRestService)
        {
            this.productRestService = producRestService;
            AddProductCommand = new DelegateCommand(AddProduct);
            UpdateProductCommand = new DelegateCommand(UpdateProduct);
            DeleteProductCommand = new DelegateCommand(DeleteProduct);
            Task.Run(() => this.Initialize()).Wait();
        }

        #endregion

        #region Methods

        private async Task Initialize()
        {
            await GetProducts();
        }

        private async Task GetProducts()
        {
            Products = new ObservableCollection<ProductModel>(await productRestService.GetAllProductsAsync());
        }

        #endregion

        #region Commands

        public DelegateCommand AddProductCommand { get; private set; }

        private async void AddProduct()
        {
            try
            {
                //get new student
                ProductModel newStudent = Products.FirstOrDefault(s => s.ProductId == Guid.Empty);

                if (newStudent == null)
                {
                    MessageBox.Show("Please enter a new product in the grid.");
                    return;
                }

                // send student 
                await productRestService.CreateProductAsync(newStudent);

                //refresh lista
                await GetProducts();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public DelegateCommand UpdateProductCommand { get; private set; }

        private async void UpdateProduct()
        {
            try
            {
                //check if selected product
                if (SelectedProduct == null)
                {
                    MessageBox.Show("Please select a product for update");
                    return;
                }

                //get selected product id
                Guid productId = selectedProduct.ProductId;

                //send request to update 
                await productRestService.UpdateProductAsync(productId, SelectedProduct);

                //refresh lista
                await GetProducts();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public DelegateCommand DeleteProductCommand { get; private set; }

        private async void DeleteProduct()
        {
            try
            {
                //check if selected product
                if (SelectedProduct == null)
                {
                    MessageBox.Show("Please select a product");
                    return;
                }

                //get selected product id
                Guid productId = selectedProduct.ProductId;

                //send request to delete Id via rest service
                await productRestService.DeleteProductAsync(productId);

                //refresh lista
                await GetProducts();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        #endregion
    }
}
