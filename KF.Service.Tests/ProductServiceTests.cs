
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
    public class ProductServiceTests
    {
        private EFCoreRepository<Product> productRepository;
        private EFCoreRepository<Stock> stockRepository;

        [OneTimeSetUp]
        public void Setup()
        {
            //Set up DbContext
            var options = new DbContextOptionsBuilder<KronsoftFarmaContext>();
            options.UseSqlServer("Server=ADISABAULAPDELL;Database=KronsoftFarma;Trusted_Connection=True;");
            KronsoftFarmaContext _dbContext = new KronsoftFarmaContext(options.Options);

            //Set up Repos
            productRepository = new(_dbContext);
            stockRepository = new(_dbContext);

            //Set up Automapper
            AutoMapperConfiguration.Init();
            AutoMapperConfiguration.MapperConfiguration.AssertConfigurationIsValid();
        }

        [Test]
        public void GetProductsTest()
        {
            //arrange
            ProductService service = GetService();

            //act
            var products = service.GetProducts();

            //assert
            Assert.That(products.Any());
            
        }

        [Test]
        public void GetProductsByIdTest()
        {
            //arrange
            ProductService service = GetService();

            //act
            String source = "3fa85f64-5717-4562-b3fc-2c963f66afa6";
            Guid result = new Guid(source);
            var product = service.GetProductById(result);

            //assert
            Assert.That(product != null);

        }

        [Test]
        public void CreateProductTest()
        {
            ProductService service = GetService();
            Guid createdProductId = Guid.Empty;
            try
            {
                //arrange
                Product product = CreateProductModel("Test", "Testing", 110, "Test");

                //act
                ProductModel createdProduct = service.CreateProduct(product.ToModel());
                createdProductId = createdProduct.ProductId;

                //assert
                Assert.That(createdProduct != null);
                Assert.That(createdProduct?.Name == product.Name);
                Assert.That(createdProduct?.Description == product.Description);
                Assert.That(createdProduct?.Price == product.Price);
                Assert.That(createdProduct?.Producer == product.Producer);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                service.RemoveProductById(createdProductId);
            }
        }

        [Test]
        public void DeleteProductTest()
        {
            //arrange
            ProductService service = GetService();
            Product product = CreateProductModel("Test", "Testing", 110, "Test");
            ProductModel createdProduct = service.CreateProduct(product.ToModel());

            //act
            service.RemoveProductById(createdProduct.ProductId);
            var deletedProduct = service.GetProductById(createdProduct.ProductId);

            //assert
            Assert.That(deletedProduct == null);
        }

        [Test]
        public void UpdateProductTest()
        {
            ProductService service = GetService();
            Guid createdProductId = Guid.Empty;

            try
            {
                //arrange
                Product product = CreateProductModel("Test", "Testing", 110, "Test");
                ProductModel createdProduct = service.CreateProduct(product.ToModel());
                createdProductId = createdProduct.ProductId;

                //act
                Setup();
                service = GetService();
                createdProduct.Name = "TTTT";
                service.UpdateProduct(createdProduct);
                var updatedProduct = service.GetProductById(createdProductId);

                //assert
                Assert.That(updatedProduct != null);
                Assert.That(updatedProduct.Name == "TTTT");

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                service.RemoveProductById(createdProductId);
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

        private ProductService GetService()
        {
            return new(productRepository, stockRepository);
        }
    }
}