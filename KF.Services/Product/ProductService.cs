using KF.Common.Model.Automapper;
using KF.CommonModel.Models;
using KF.Core.Data;
using KF.Core.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace KF.Services.Product
{
    public class ProductService : IProductService
    {
        #region Fields
        private readonly IRepository<Core.DomainModels.Product> productRepository;
        private readonly IRepository<Core.DomainModels.Stock> stockRepository;

        #endregion

        #region ctor
        public ProductService(IRepository<Core.DomainModels.Product> productRepository,
                              IRepository<Core.DomainModels.Stock> stockRepository
            )
        {
            this.productRepository = productRepository;
            this.stockRepository = stockRepository;
        }

        #endregion

        #region Methods

        public ProductModel GetProductById(Guid productId)
        {
            try
            {
                var product = productRepository.Table.FirstOrDefault(s => s.ProductId == productId);
                return product.ToModel();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public IEnumerable<ProductModel> GetProducts()
        {
            try
            {
                var products = productRepository.Table.Include(p => p.Stock).Select(x => x.ToModel()).ToList();
                return products;

            } catch(Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public ProductModel CreateProduct(ProductModel product)
        {
            try
            {
                if (product == null)
                    throw new ArgumentNullException("Exception product is null");

                KF.Core.DomainModels.Product productEntity = product.ToEntity();
                productEntity.Stock = new KF.Core.DomainModels.Stock();
                productRepository.Insert(productEntity);

                ProductModel createdProduct = GetProductById(productEntity.ProductId);
                return createdProduct;

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public bool RemoveProductById(Guid productId)
        {
            try
            {
                var productEntity = productRepository.Table.FirstOrDefault(x => x.ProductId == productId);

                if (productEntity == null) return false;

                productRepository.Delete(productEntity);

                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public ProductModel UpdateProduct(ProductModel product)
        {
            try
            {
                if (product == null)
                    throw new ArgumentNullException("Exception product is null");

                var productEntity = productRepository.TableNoTracking.FirstOrDefault(s => s.ProductId == product.ProductId);
                if (productEntity == null) return null;

                productRepository.Update(product.ToEntity());
                return GetProductById(product.ProductId);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        #endregion
    }
}
