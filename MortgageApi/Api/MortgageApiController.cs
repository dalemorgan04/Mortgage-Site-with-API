using Microsoft.AspNetCore.Mvc;
using MortgageApi.Api.Entities;
using MortgageApi.Models;
using MortgageApi.Models.Enums;
using MortgageApi.Services.CustomerNS;
using MortgageApi.Services.MortgageNS;
using MortgageApi.Services.SearchNS;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MortgageApi.API
{
    [Route("api/mortgage")]
    [ApiController]
    public class MortgageApiController : ControllerBase
    {
        private readonly IMortgageService _mortgageService;
        private readonly ICustomerService _customerSevice;
        private readonly ISearchService _searchService;

        public MortgageApiController(ICustomerService customerSevice, IMortgageService mortgageService, ISearchService searchService)
        {
            _mortgageService = mortgageService;
            _customerSevice = customerSevice;
            _searchService = searchService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(
            [FromQuery(Name = "customer_id")] Guid customerId,
            [FromQuery(Name = "property_value")] decimal propertyValue,
            decimal deposit,
            [FromQuery(Name = "mortgage_type")] int mortgageType)
        {
            try
            {
                var customer = await _customerSevice.GetCustomer(customerId);
                if (customer == null)
                {
                    return NotFound("Customer not found");
                }

                var searchRequest = new SearchRequest()
                {
                    CustomerId = customer.Id,
                    PropertyValue = propertyValue,
                    Deposit = deposit,
                    MortgageTypeInt = mortgageType
                };
                if (!searchRequest.IsValid)
                {
                    return BadRequest(searchRequest.GetValidationMessage);
                }

                var search = new Search()
                {
                    Customer = customer,
                    PropertyValue = searchRequest.PropertyValue,
                    Deposit = searchRequest.Deposit,
                    MortgageType = (MortgageTypeIdentifier)searchRequest.MortgageTypeInt
                };

                var results = await _searchService.FindMortgages(search, true);
                return Ok(results);
            }
            catch (Exception e)
            {
                return new JsonResult(new { error = e.Message });
            }
        }

        //Tried both get and post to compare primitive and complex parameters
        //Also compared Json Result to HttpAction
        [HttpPost("search/post")]
        public async Task<JsonResult> SearchPost([FromBody] string value)
        {
            try
            {
                var searchRequest = JsonConvert.DeserializeObject<SearchRequest>(value);
                var customer = await _customerSevice.GetCustomer(searchRequest.CustomerId);
                if (customer == null)
                {
                    return new JsonResult(new { error = "Customer not found" });
                }

                var search = new Search()
                {
                    Customer = customer,
                    PropertyValue = searchRequest.PropertyValue,
                    Deposit = searchRequest.Deposit,
                    MortgageType = (MortgageTypeIdentifier)searchRequest.MortgageTypeInt
                };

                var results = await _searchService.FindMortgages(search, true);

                return new JsonResult(JsonConvert.SerializeObject(results));
            }
            catch (Exception e)
            {
                return new JsonResult(new { error = e.Message });
            }
        }
    }
}