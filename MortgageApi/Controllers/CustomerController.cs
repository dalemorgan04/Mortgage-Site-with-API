using Microsoft.AspNetCore.Mvc;
using MortgageApi.Models;
using MortgageApi.Services.CustomerNS;
using MortgageApi.ViewModels.CustomerNS;
using System;
using System.Threading.Tasks;

namespace MortgageApi.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("~/about-you", Name = "GetCreate")]
        public IActionResult GetCreate()
        {
            return View("Create");
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,DOB,Email")] CustomerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var customer = new Customer()
                {
                    Email = viewModel.Email,
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    DOB = viewModel.DOB
                };

                Guid id = await _customerService.CreateCustomer(customer);
                return RedirectToAction("GetSearchCriteria","Search", new { id=id });
            }
            return View(viewModel);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> Index()
        {
            return View(await _customerService.GetCustomers());
        }
    }
}