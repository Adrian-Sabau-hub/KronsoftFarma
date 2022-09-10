using KF.CommonModel.Models;
using KF.Services.Product;
using KF.Services.Stock;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace KF.WebApi.Controllers
{
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

        // Get by id

        [Route("/api/Products/{id}")]
        [HttpGet]
        public ProductModel GetById([FromRoute] Guid id)
        {
            try
            {
                var product = _productService.GetProductById(id);

                return product;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
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

        // Create

        [Route("/api/Products")]
        [HttpPost]
        public ProductModel Create([FromBody] ProductModel product)
        {
            try
            {
                //var studentModel = JsonSerializer.Deserialize<StudentModel>(student);
                ProductModel createdProduct = _productService.CreateProduct(product);
                return createdProduct;
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
                var stockData = _stockService.GetStocks();

                var stock = stockData.FirstOrDefault(s => s.ProductId == id);

                if(stock != null)
                    _stockService.RemoveStockById(stock.StockId);

                _productService.RemoveProductById(id);

                return true;
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
                ProductModel updatedProduct = _productService.UpdateProduct(product);
                return updatedProduct;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

    }
}
