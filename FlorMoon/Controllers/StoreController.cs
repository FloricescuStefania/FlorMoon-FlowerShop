using FlorMoon.Models;
using FlorMoon.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlorMoon.Controllers
{
    public class StoreController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public StoreController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult Index(string category, string sort)
        {
            var products = _productService.GetAllProducts();

            // Filtering by category 
            if (!string.IsNullOrEmpty(category))
            {
                products = products.Where(p => p.Categories?.CategoryName == category).ToList();
            }

            // Sort by price
            products = sort switch
            {
                "price_asc" => products.OrderBy(p => p.Price).ToList(),
                "price_desc" => products.OrderByDescending(p => p.Price).ToList(),
                _ => products
            };

            ViewBag.Categories = _categoryService.GetAllCategories();
            ViewBag.CurrentCategory = category;
            ViewBag.CurrentSort = sort;

            return View(products);
        }

        // --- Search function ---
        public IActionResult Search(string searchTerm)
        {
            var products = _productService.GetAllProducts();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = products.Where(p =>
                    (!string.IsNullOrEmpty(p.ProductName) && p.ProductName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(p.ProductDescription) && p.ProductDescription.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                ).ToList();
            }

            ViewBag.SearchTerm = searchTerm;
            return View(products);
        }

        [Route("Store/Product/{id}")]
        public IActionResult Product(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null) return NotFound();

            return View(product);
        }
    }
}