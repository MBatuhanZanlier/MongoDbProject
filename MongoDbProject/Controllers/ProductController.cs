using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDbProject.Dtos.ProductDtos;
using MongoDbProject.Services.CategoryServices;
using MongoDbProject.Services.GoogleCloud;
using MongoDbProject.Services.ProductServices;

namespace MongoDbProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService; 
        private readonly ICategoryService _categoryService; 
        private readonly ICloudStorageService _cloudStorageService;

        public ProductController(IProductService productService, ICategoryService categoryService, ICloudStorageService cloudStorageService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _cloudStorageService = cloudStorageService;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _productService.GetProductsWithCategoryAsync();
           
            return View(values);
        }
     
        [HttpGet]
        public async Task<IActionResult> CreatProduct()
        {
            List<SelectListItem> values = (from x in await _categoryService.GetAllCategoryAsync()
                                           select new SelectListItem
                                           {
                                               Text = x.CategoryName,
                                               Value = x.CategoryId.ToString()
                                           }).ToList();
            ViewBag.Categories = values;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreatProduct(CreateProductDto productDto)
        {

            if (productDto.ImageUrl != null)
            {
                productDto.SavedFileName = GenerateFileNameToSave(productDto.ImageUrl.FileName);
                productDto.SavedUrl = await _cloudStorageService.UploadFileAsync(productDto.ImageUrl, productDto.SavedFileName);
            }
             

            
            await _productService.CreateProductAsync(productDto);
            return RedirectToAction("Index");
        }
        private string? GenerateFileNameToSave(string incomingFileName)
        {
            var fileName = Path.GetFileNameWithoutExtension(incomingFileName);
            var extension = Path.GetExtension(incomingFileName);
            return $"{fileName}-{DateTime.Now.ToUniversalTime().ToString("yyyyMMddHHmmss")}{extension}";
        }
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(string id)
        {

            List<SelectListItem> values = (from x in await _categoryService.GetAllCategoryAsync()
                                           select new SelectListItem
                                           {
                                               Text = x.CategoryName,
                                               Value = x.CategoryId.ToString()
                                           }).ToList();
            ViewBag.Categories = values;
            var value = await _productService.GetByIdProductAsync(id);
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            await _productService.UpdateProductAsync(updateProductDto);
            return RedirectToAction("ProductList");
        }


        public async Task<IActionResult> GetProductwithCategoryList()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pdfproduct/" + "productlist.pdf");
            var stream = new FileStream(path, FileMode.Create);

            Document document = new Document(PageSize.A4);
            PdfWriter.GetInstance(document, stream);

            document.Open();
            PdfPTable pdfPTable = new PdfPTable(3);
            pdfPTable.AddCell("Ürün Adı");
            pdfPTable.AddCell("Ürün Kategorisi");
            pdfPTable.AddCell("Ürün Stok Adedi");

            var model = await _productService.GetProductsWithCategoryAsync();
            foreach (var item in model)
            {
                pdfPTable.AddCell(item.Name);
                pdfPTable.AddCell(item.Category.CategoryName);
                pdfPTable.AddCell(item.Stock.ToString());
            }

            document.Add(pdfPTable);
            document.Close();
            return File("/pdfproduct/productlist.pdf", "application/pdf", "productlist.pdf");
        }
    }
}
