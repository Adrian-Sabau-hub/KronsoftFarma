using KF.Common.Model.Automapper;
using KF.CommonModel.Models;
using KF.Core.Data;
using KF.Core.DomainModels;
using KF.Services.Product;
using KF.Services.Stock;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;

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
        public void GetStockByProductIdTest()
        {
            //arrange
            StockService service = GetService();

            //act
            String productId = "3fa85f64-5717-4562-b3fc-2c963f66afa6";
            Guid result = new Guid(productId);
            var stock = service.GetStockByProductId(result);

            //assert
            Assert.IsNotNull(stock);
            //Assert.That(stock != null);

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
                Stock stock = CreateStockModel(createdProductId,999);

                //act
                StockModel createdStock = service.CreateStock(stock.ToModel());
                createdStockId = createdStock.StockId;

                //assert
                Assert.IsNotNull(createdStock);
                //Assert.That(createdStock != null);
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
                Stock stock = CreateStockModel(createdProductId, 999);
                StockModel createdStock = service.CreateStock(stock.ToModel());
                createdStockId = createdStock.StockId;

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
                Stock stock = CreateStockModel(createdProductId,999);
                StockModel createdStock = service.CreateStock(stock.ToModel());
                createdStockId = createdStock.StockId;

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