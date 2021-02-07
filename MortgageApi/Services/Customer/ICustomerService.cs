using MortgageApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MortgageApi.Services.CustomerNS
{
    public interface ICustomerService
    {
        public Task<Guid> CreateCustomer(Customer customer);

        public Task<List<Customer>> GetCustomers();

        public Task<Customer> GetCustomer(Guid id);

        public Task<bool> EditCustomer(Customer customer);

        public Task<bool> Delete(Guid id);
    }
}