using KF.CommonModel.Models;

namespace KF.Services.Product
{
    public interface IProductService
    {
        IEnumerable<ProductModel> GetProducts();
        ProductModel GetProductById(Guid productId);
        bool RemoveProductById(Guid productId);
        ProductModel CreateProduct(ProductModel product);
        ProductModel UpdateProduct(ProductModel product);
    }
}
