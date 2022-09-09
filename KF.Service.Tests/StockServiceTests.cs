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
                service.RemoveStockById(createdStock.StockId);
                var deletedStock = service.GetStockById(createdStock.StockId);

                //assert
                Assert.IsNull(createdStock);
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
            Guid createdStockId = Guid.Empty;

            try
            {
                //arrange
                String productId = "3FA85F64-5717-4562-B3FC-2C963F66AFA6";
                Guid result = new Guid(productId);
                Stock stock = CreateStockModel(result,999);
                StockModel createdStock = service.CreateStock(stock.ToModel());
                createdStockId = createdStock.StockId;

                //act
                Setup();
                service = GetService();
                createdStock.Quantity = 888;
                service.UpdateStock(createdStock);
                var updatedStock = service.GetStockById(createdStockId);

                //assert
                Assert.That(updatedStock != null);
                //Assert.That(updatedStock.ProductId == stock.ProductId);
                Assert.That(updatedStock.Quantity == 888);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                service.RemoveStockById(createdStockId);
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