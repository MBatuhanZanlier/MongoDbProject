using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using MongoDbProject.Dtos.CustomerDtos;
using MongoDbProject.Services.CustomerServices;

namespace MongoDbProject.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        public async Task<IActionResult> Index()
        {
            var values = await _customerService.GetAllCustomerAsync();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateCustomer()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDto customerDto)
        {
            await _customerService.CreateCustomerAsync(customerDto);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            await _customerService.DeleteCustomerAsync(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateCustomer(string id)
        {
            var values = await _customerService.GetByIdCustomerAsync(id);
            return View(values);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerDto customerDto)
        {
            await _customerService.UpdateCustomerAsync(customerDto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> CustomerExcellDowland()
        {
            using (var workBook = new XLWorkbook())
            {
                var workSheet = workBook.Worksheets.Add("Müşteri Tablosu");
                workSheet.Cell(1, 1).Value = "Müşteri Ad Soyad";
                workSheet.Cell(1, 2).Value = "Mail Adresi";
                workSheet.Cell(1, 3).Value = "Şehir";
                workSheet.Cell(1, 4).Value = "Telefon No";
                var model = await _customerService.GetAllCustomerAsync();

                int rowCount = 2;
                foreach (var item in model)
                {
                    workSheet.Cell(rowCount, 1).Value = item.CustomerNameSurname;
                    workSheet.Cell(rowCount, 2).Value = item.CustomerMail;
                    workSheet.Cell(rowCount, 3).Value = item.CustomerCity;
                    workSheet.Cell(rowCount, 4).Value = item.CustomerPhone;
                    rowCount++;
                }
                using (var stream = new MemoryStream())
                {
                    workBook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "customerlist.xlsx");
                }

            }

        }
    }
}
