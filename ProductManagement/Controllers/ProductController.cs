using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductManagement.Repository.Entities;
using ProductManagement.Repository.Interface;
using System;
using System.Globalization;
using System.Linq;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace ProductManagement.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        public ProductController(IProductService productService) 
        {
            this.productService = productService;
        }
        /// <summary>
        /// Index
        /// </summary>
        /// <returns> Return To index view </returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Add Product 
        /// </summary>
        /// <param name="Id"> Passing Product Id</param>
        /// <returns> Return to Index </returns>
        [HttpGet]
        public IActionResult AddProduct(int Id = 0)
        {
            try
            {
                if (Id == 0)
                {
                    ProductModel model = new ProductModel();
                    return View(model);
                }
                var result = productService.GetById(Id);
                if (result.ProductID == 0)
                {
                    TempData["ErrorMessage"] = $"Product details not foiund with id: {Id}";
                    return RedirectToAction("Index");
                }
                return View(result);

            }
            catch ( Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();

            }
        }
        /// <summary>
        /// Update and insert
        /// </summary>
        /// <param name="model"> Product Model</param>
        /// <returns> Redirect to Index</returns>
        [HttpPost]
        public IActionResult UpsertProduct(ProductModel model)
        {
            try
            {
                bool result = productService.UpsertProduct(model);
                if(!result)
                {
                    if(model.ProductID == 0)
                    {
                        TempData["ErrorMessage"] = "Unable to insert data";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to update data";
                    }
                    return View();
                }
                if(model.ProductID == 0) 
                {
                    TempData["successMessage"] = "Product deatils to inserted";
                }
                else
                {
                    TempData["successMessage"] = "Product deatils to updated";
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
        /// <summary>
        /// List of Products
        /// </summary>
        /// <param name="keyword"> Search keyword </param>
        /// <returns> Json list </returns>
        [HttpGet]
        public JsonResult ProductList(string keyword)
        {
            try
            {
                var products = productService.GetAll();
                if (DateTime.TryParse(keyword, out DateTime date))
                {
                    // If successful, filter by date
                    products = products.Where(p => p.CreationDate.Date == date.Date);
                }
                else
                {
                    // If not a valid date, treat as a regular keyword search
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        keyword = keyword.ToLower();
                        products = products.Where(p =>
                            p.Code.ToLower().Contains(keyword) ||
                        p.Name.ToLower().Contains(keyword) ||
                            p.Description.ToLower().Contains(keyword) ||
                            p.Category.ToLower().Contains(keyword)
                        );
                    }
                }
                var newList = products.OrderByDescending(x => x.CreationDate).ToList();

                return new JsonResult(newList);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
        /// <summary>
        /// Delete Record
        /// </summary>
        /// <param name="productId"> Product Id </param>
        /// <returns> Json result </returns>
        public JsonResult Delete(int productId)
        {
            try
            {
                productService.DeleteProduct(productId);
                return new JsonResult("Data Deleted");
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
    }
}
