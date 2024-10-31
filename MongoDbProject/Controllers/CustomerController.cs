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

    }
}
