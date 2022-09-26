using KF.Common.Model.Automapper;
using KF.CommonModel.Models;
using KF.Core.Data;
using KF.Core.DomainModels;
using KF.Services.Product;
using KF.Services.Stock;
using Microsoft.EntityFrameworkCore;

namespace KF.Service.Tests
{
    public class StockServiceTests
    {
        private EFCoreRepository<Stock> stockRepository;
        private EFCoreRepository<Product> productRepository;

        [OneTimeSetUp]
        public void Setup()
        {
            //Set up DbContext
            var options = new DbContextOptionsBuilder<KronsoftFarmaContext>();
            options.UseSqlServer("Server=ADISABAULAPDELL;Database=KronsoftFarma;Trusted_Connection=True;");
            KronsoftFarmaContext _dbContext = new KronsoftFarmaContext(options.Options);

            //Set up Repos
            stockRepository = new(_dbContext);
            productRepository = new(_dbContext);

            //Set up Automapper
            AutoMapperConfiguration.Init();
            AutoMapperConfiguration.MapperConfiguration.AssertConfigurationIsValid();
        }

        [Test]
        public void GetStockTest()
        {
            //arrange
            StockService service = GetService();

            //act
            var stocks = service.GetStocks();

            //assert
            Assert.That(stocks.Any());

        }

        [Test]
        public void GetStockByIdTest()
        {
            //arrange
            StockService service = GetService();

            //act
            String source = "BC48BD3F-BC75-44CA-8FDA-2C0AB68C7379";
            Guid result = new Guid(source);
            var stock = service.GetStockById(result);

            //assert
            Assert.That(stock != null);

        }

        [Test]
        public void GetStockByProductIdTest()
        {
            //arrange
            StockService service = GetService();
            String productId = "3fa85f64-5717-4562-b3fc-2c963f66afa6";
            Guid result = new Guid(productId);

            //act
            var stock = service.GetStockByProductId(result);

            //assert
            Assert.IsNotNull(stock);
        }

        [Test]
        public void CreateStockTest()
        {
            StockService service = GetService();
            ProductService serviceProduct = GetServiceProduct();
            Guid createdStockId = Guid.Empty;
            Guid createdProductId = Guid.Empty;

            try
            {
                //arrange
                Product product = CreateProductModel("Test", "Testing", 110, "Test");
                ProductModel createdProduct = serviceProduct.CreateProduct(product.ToModel());
                createdProductId = createdProduct.ProductId;

                //act
                Stock newStock;
                StockModel createdStock = service.GetStockByProductId(createdProductId);

                if (createdStock == null)
                {
                    newStock = CreateStockModel(createdProductId, 999);
                    createdStock = service.CreateStock(newStock.ToModel());
                    createdStockId = createdStock.StockId;
                }
                else
                {
                    createdStockId = createdStock.StockId;

                }

                //assert
                Assert.IsNotNull(createdStock);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                service.RemoveStockById(createdStockId);
                serviceProduct.RemoveProductById(createdProductId);
            }
        }

        [Test]
        public void UpdateStockTest()
        {
            StockService service = GetService();
            ProductService serviceProduct = GetServiceProduct();
            Guid createdStockId = Guid.Empty;
            Guid createdProductId = Guid.Empty;

            try
            {
                //arrange
                Product product = CreateProductModel("Test", "Testing", 110, "Test");
                ProductModel createdProduct = serviceProduct.CreateProduct(product.ToModel());
                createdProductId = createdProduct.ProductId;

                StockModel createdStock = service.GetStockByProductId(createdProductId);

                if (createdStock == null)
                {
                    Stock newStock = CreateStockModel(createdProductId, 999);
                    createdStock = service.CreateStock(newStock.ToModel());
                    createdStockId = createdStock.StockId;
                }
                else
                {
                    createdStockId = createdStock.StockId;

                }

                //act
                Setup();
                service = GetService();
                serviceProduct = GetServiceProduct();
                createdStock.Quantity = 888;
                service.UpdateStock(createdStock);
                var updatedStock = service.GetStockById(createdStockId);

                //assert
                Assert.IsNotNull(updatedStock);
                Assert.That(updatedStock.Quantity, Is.EqualTo(888));

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                service.RemoveStockById(createdStockId);
                serviceProduct.RemoveProductById(createdProductId);
            }

        }

        [Test]
        public void DeleteStockTest()
        {
            StockService service = GetService();
            ProductService serviceProduct = GetServiceProduct();
            Guid createdStockId = Guid.Empty;
            Guid createdProductId = Guid.Empty;

            try
            {
                //arrange
                Product product = CreateProductModel("Test", "Testing", 110, "Test");
                ProductModel createdProduct = serviceProduct.CreateProduct(product.ToModel());
                createdProductId = createdProduct.ProductId;
                
                StockModel stock = service.GetStockByProductId(createdProductId);

                if(stock == null)
                {
                    Stock newStock = CreateStockModel(createdProductId, 999);
                    StockModel createdStock = service.CreateStock(newStock.ToModel());
                    createdStockId = createdStock.StockId;
                }
                else
                {
                    createdStockId = stock.StockId;

                }

                //act
                service.RemoveStockById(createdStockId);
                var deletedStock = service.GetStockById(createdStockId);

                //assert
                Assert.IsNull(deletedStock);
                //Assert.That(deletedStock == null);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                serviceProduct.RemoveProductById(createdProductId);
            }

        }

        private Product CreateProductModel(string name, string description, decimal price, string producer)
        {
            KF.Core.DomainModels.Product product = new()
            {
                Name = name,
                Description = description,
                Price = price,
                Producer = producer,
            };

            return product;
        }

        private Stock CreateStockModel(Guid productId, float quantity)
        {
            KF.Core.DomainModels.Stock stock = new()
            {
                StockId = new Guid("9BD732EE-A523-4F76-B9E8-437B4EBBFE0A"),
                ProductId = productId,
                Quantity = quantity,
            };

            return stock;
        }

        private StockService GetService()
        {
            return new(productRepository, stockRepository);
        }

        private ProductService GetServiceProduct()
        {
            return new(productRepository, stockRepository);
        }
    }
}