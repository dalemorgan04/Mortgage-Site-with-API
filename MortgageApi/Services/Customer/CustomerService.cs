using Microsoft.EntityFrameworkCore;
using MortgageApi.Data;
using MortgageApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MortgageApi.Services.CustomerNS
{
    public class CustomerService : ICustomerService
    {
        private readonly DataContext _context;
        public CustomerService(DataContext context)
        {
            _context = context;
        }

        public async Task<Customer> GetCustomer(Guid id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<List<Models.Customer>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Guid> CreateCustomer(Customer customer)
        {
            _context.Add(customer);
            await _context.SaveChangesAsync();
            return customer.Id;
        }

        public async Task<bool> EditCustomer(Models.Customer customer)
        {
            try
            {
                _context.Update(customer);
                await _context.SaveChangesAsync();
                return true;
            }catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(customer.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }

        private bool CustomerExists(Guid id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }

    }
}
