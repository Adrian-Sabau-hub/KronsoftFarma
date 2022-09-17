using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using WPF.Client.APIClient.RestServices;
using WPF.Client.Models;

namespace WPF.Client.ViewModels
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

        #region Commands

        public DelegateCommand AddProductCommand { get; private set; }

        private async void AddProduct()
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

        public DelegateCommand UpdateProductCommand { get; private set; }

        private async void UpdateProduct()
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

        public DelegateCommand DeleteProductCommand { get; private set; }

        private async void DeleteProduct()
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

        #endregion

        public ProductViewViewModel(ProductRestService producRestService)
        {
            this.productRestService = producRestService;
            AddProductCommand = new DelegateCommand(AddProduct);
            UpdateProductCommand = new DelegateCommand(UpdateProduct);
            DeleteProductCommand = new DelegateCommand(DeleteProduct);
            Task.Run(() => this.Initialize()).Wait();
        }

        private async Task Initialize()
        {
            await GetProducts();
        }

        private async Task GetProducts()
        {
            Products = new ObservableCollection<ProductModel>(await productRestService.GetAllProductsAsync());
        }

        

        



        


    }
}
