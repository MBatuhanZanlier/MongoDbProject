using Microsoft.AspNetCore.Mvc;
using MongoDbProject.Dtos.ProductDtos;
using MongoDbProject.Services.CategoryServices;
using MongoDbProject.Services.ProductServices;

namespace MongoDbProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var values = _productService.GetAllProductAsync();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreatProduct()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreatProduct(CreateProductDto productDto)
        {
            await _productService.CreateProductAsync(productDto);
            return RedirectToAction("Index");
        } 

        
    }
}
