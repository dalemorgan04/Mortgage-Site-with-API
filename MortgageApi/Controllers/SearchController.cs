using Microsoft.AspNetCore.Mvc;
using MortgageApi.Models;
using MortgageApi.Models.Enums;
using MortgageApi.Services.CustomerNS;
using MortgageApi.Services.MortgageNS;
using MortgageApi.Services.SearchNS;
using MortgageApi.ViewModels.MortgageNS;
using MortgageApi.ViewModels.SearchNS;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MortgageApi.Controllers
{
    public class SearchController : Controller
    {
        private readonly IMortgageService _mortgageService;
        private readonly ICustomerService _customerSevice;
        private readonly ISearchService _searchService;

        public SearchController(ICustomerService customerSevice, IMortgageService mortgageService, ISearchService searchService)
        {
            _mortgageService = mortgageService;
            _customerSevice = customerSevice;
            _searchService = searchService;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("~/about-your-property", Name = "GetSearchCriteria")]
        public async Task<IActionResult> GetSearchCriteria([FromQuery(Name = "id")] Guid customerId)
        {
            var customer = await _customerSevice.GetCustomer(customerId);
            if (customer == null)
            {
                return NotFound();
            }

            var viewModel = new SearchViewModel(customer);
            return View("Criteria", viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<JsonResult> CreateSearch([FromBody] SearchParametersViewModel searchCriteria)
        {
            if (ModelState.IsValid)
            {
                var search = new Search()
                {
                    Customer = await _customerSevice.GetCustomer(searchCriteria.CustomerId),
                    Deposit = searchCriteria.Deposit,
                    PropertyValue = searchCriteria.PropertyValue,
                    MortgageType = (MortgageTypeIdentifier)searchCriteria.MortgageType
                };
                var searchId = await _searchService.CreateSearch(search);
                return new JsonResult(new { searchId = searchId });
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return new JsonResult("");
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("~/your-search-results", Name = "GetSearchResults")]
        public async Task<IActionResult> GetSearchResults([FromQuery(Name = "id")] int searchId)
        {
            var search = await _searchService.GetSearch(searchId);
            if (search == null)
            {
                return NotFound();
            }
            var mortgages = await _searchService.FindMortgages(search);

            var viewModel = new SearchResultsViewModel(search, mortgages);
            return View("SearchResults", viewModel);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("~/interested", Name = "GetSearchDetails")]
        public async Task<IActionResult> GetDetails([FromQuery(Name ="sid")] int searchId, [FromQuery(Name ="mid")]int mortgageId)
        {
            //Register that the customer is interested in this product based on this search for potential reporting purposes
            var mortgage = await _searchService.RegisterMortgageOfInterest(searchId, mortgageId);
            var viewModel = new SearchDetailsViewModel(mortgage, searchId);
            return View("Details", viewModel);
        }
    }
}