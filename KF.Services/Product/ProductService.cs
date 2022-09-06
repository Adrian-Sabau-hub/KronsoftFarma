using KF.Common.Model.Automapper;
using KF.CommonModel.Models;
using KF.Core.Data;

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
            var product = productRepository.Table.FirstOrDefault(s => s.ProductId == productId);
            return product.ToModel();
        }

        public IEnumerable<ProductModel> GetProducts()
        {
            var products = productRepository.Table.Select(x => x.ToModel()).ToList();
            return products;
        }

        public ProductModel CreateProduct(ProductModel product)
        {
            if (product == null)
                throw new ArgumentNullException("Exception product is null");

            KF.Core.DomainModels.Product productEntity = product.ToEntity();
            productRepository.Insert(productEntity);

            ProductModel createdProduct = GetProductById(productEntity.ProductId);
            return createdProduct;
        }

        public bool RemoveProductById(Guid productId)
        {
            var productEntity = productRepository.Table.FirstOrDefault(x => x.ProductId == productId);
            
            if (productEntity == null)  return false;

            productRepository.Delete(productEntity);

            return true;
        }

        public ProductModel UpdateProduct(ProductModel product)
        {
            if (product == null)
                throw new ArgumentNullException("Exception product is null");
            
            var productEntity = productRepository.TableNoTracking.FirstOrDefault(s => s.ProductId == product.ProductId);
            if (productEntity == null) return null;

            productRepository.Update(product.ToEntity());
            return GetProductById(product.ProductId);
        }

        #endregion
    }
}
