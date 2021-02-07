using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MortgageApi.Data;
using MortgageApi.Services.CustomerNS;
using MortgageApi.Services.MortgageNS;
using MortgageApi.ViewModels.MortgageNS;
using System;
using System.Threading.Tasks;

namespace MortgageApi.Controllers
{
    public class MortgageController : Controller
    {
        private readonly DataContext _context;
        private readonly IMortgageService _mortgageService;
        private readonly ICustomerService _customerSevice;

        public MortgageController(DataContext context, ICustomerService customerSevice, IMortgageService mortgageService)
        {
            _context = context;
            _mortgageService = mortgageService;
            _customerSevice = customerSevice;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> Index()
        {
            var mortgages = await _mortgageService.GetMortgages();
            var viewModel = new MortgagesViewModel(mortgages);
            return View(viewModel);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mortgage = await _context.Mortgages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mortgage == null)
            {
                return NotFound();
            }

            return View(mortgage);
        }
    }
}