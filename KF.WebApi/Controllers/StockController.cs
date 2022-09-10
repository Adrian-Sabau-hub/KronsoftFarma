using KF.CommonModel.Models;
using KF.Core.DomainModels;
using KF.Services.Product;
using KF.Services.Stock;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using static NuGet.Packaging.PackagingConstants;

namespace KF.WebApi.Controllers
{
    [ApiController]

    public class StockController : Controller
    {
        IStockService _stockService;
        IProductService _productService;

        public StockController(IStockService stockService, IProductService productService)
        {
            _stockService = stockService;
            _productService = productService;
        }

        // Get

        [Route("/api/Stocks")]
        [HttpGet]
        public IEnumerable<StockModel> GetAll()
        {
            try
            {
                var stocks = _stockService.GetStocks();
                var products = _productService.GetProducts();
                var result = (from p in products
                              join s in stocks on p.ProductId equals s.ProductId
                              select new StockModel
                              {
                                  StockId = s.StockId,
                                  ProductId = p.ProductId,
                                  ProductName = p.Name,
                                  Quantity = s.Quantity
                              }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        // Get stock by product id

        [Route("/api/Stocks/{productId}")]
        [HttpGet]
        public StockModel GetStockByProductId([FromRoute] Guid productId)
        {
            try
            {
                StockModel stock = _stockService.GetStockByProductId(productId);
                ProductModel product = _productService.GetProductById(productId);
                stock.ProductName = product.Name;   
                return stock;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        // Update

        [Route("/api/Stocks/{id}")]
        [HttpPut]
        public StockModel Update(Guid id, [FromBody] StockModel stock)
        {
            try
            {
                StockModel updatedStock = _stockService.UpdateStock(stock);
                return updatedStock;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        // Create

        [Route("/api/Stocks")]
        [HttpPost]
        public StockModel Create([FromBody] StockModel stock)
        {
            try
            {
                StockModel createdStock = _stockService.CreateStock(stock);
                return createdStock;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        // Delete

        [Route("/api/Stocks/{id}")]
        [HttpDelete]
        public bool RemoveStockById(Guid id)
        {
            try
            {
                _stockService.RemoveStockById(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            //return false;
        }

    }
}
