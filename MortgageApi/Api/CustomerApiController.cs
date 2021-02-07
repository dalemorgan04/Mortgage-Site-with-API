using Microsoft.AspNetCore.Mvc;
using MortgageApi.Api.Entities;
using MortgageApi.Models;
using MortgageApi.Models.Enums;
using MortgageApi.Services.CustomerNS;
using MortgageApi.Services.MortgageNS;
using MortgageApi.Services.SearchNS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MortgageApi.API
{
    [ApiController]
    [Route("api/customer")]
    public class CustomerApiController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerApiController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCustomer([FromBody] string value)
        {
            try
            {
                var requestCustomer = JsonConvert.DeserializeObject<CreateCustomerRequest>(value);
                if (!requestCustomer.IsValid)
                {
                    return BadRequest(requestCustomer.GetValidationMessage);
                }

                var customer = new Customer()
                {
                    FirstName = requestCustomer.FirstName,
                    LastName = requestCustomer.LastName,
                    Email = requestCustomer.Email,
                    DOB = requestCustomer.DOB
                };
                Guid customerId = await _customerService.CreateCustomer(customer);
                return  Accepted( new { customer_id = customerId });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}