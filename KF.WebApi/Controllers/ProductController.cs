using KF.CommonModel.Models;
using KF.Core.DomainModels;
using KF.Services.Product;
using KF.Services.Stock;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KF.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    //[Route("[/api/v1]")]

    public class ProductController : Controller
    {
        IStockService _stockService;
        IProductService _productService;

        public ProductController(IProductService productService, IStockService stockService)
        {
            _productService = productService;
            _stockService = stockService;
        }

        // Get

        [Route("/api/Products")]
        [HttpGet]
        public IEnumerable<ProductModel> Get()
        {
            try
            {
                var products = _productService.GetProducts();

                return products;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        // Get by id

        [Route("/api/Products/{id}")]
        [HttpGet]
        public ProductModel GetById([FromRoute] Guid id)
        {
            try
            {
                if (id != Guid.Empty)
                {
                    var product = _productService.GetProductById(id);
                    return product;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
        
        // Create

        [Route("/api/Products")]
        [HttpPost]
        public ProductModel Create([FromBody] ProductModel product)
        {
            try
            {
                if(product != null)
                {
                    ProductModel createdProduct = _productService.CreateProduct(product);
                    //StockModel newStock = new StockModel(Guid.Empty, createdProduct.ProductId, 0);
                    return createdProduct;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        // Delete

        [Route("/api/Products/{id}")]
        [HttpDelete]
        public bool RemoveProductById(Guid id)
        {
            try
            {
                if (id != Guid.Empty)
                {
                    var stockData = _stockService.GetStocks();
                    var stock = stockData.FirstOrDefault(s => s.ProductId == id);

                    if (stock != null)
                        _stockService.RemoveStockById(stock.StockId);

                    _productService.RemoveProductById(id);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        // Update

        [Route("/api/Products/{id}")]
        [HttpPut]
        public ProductModel Update(Guid id, [FromBody] ProductModel product)
        {
            try
            {
                if (product != null && id != Guid.Empty)
                {
                    ProductModel updatedProduct = _productService.UpdateProduct(product);
                    return updatedProduct;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

    }
}
