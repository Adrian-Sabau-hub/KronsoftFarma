using KF.CommonModel.Models;
using KF.Services.Product;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace KF.WebApi.Controllers
{
    [ApiController]
    //[Route("[/api/v1]")]

    public class ProductController : Controller
    {
        IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // Get by id

        [Route("/api/Products/{id}")]
        [HttpGet]
        public ProductModel GetById([FromQuery] Guid id)
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
                _productService.RemoveProductById(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            //return false;
        }

        // Update

        [Route("/api/Products/{id}")]
        [HttpPut]
        public ProductModel Update(Guid id, [FromBody] JsonElement product)
        {
            try
            {
                var productModel = JsonSerializer.Deserialize<ProductModel>(product);
                ProductModel updatedProduct = _productService.UpdateProduct(productModel);
                return updatedProduct;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        //// GET: ProductController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: ProductController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: ProductController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: ProductController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
