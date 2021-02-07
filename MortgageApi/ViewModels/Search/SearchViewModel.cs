using MortgageApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MortgageApi.ViewModels.SearchNS
{
    public class SearchViewModel
    {
        public SearchViewModel(Customer customer)
        {
            CustomerId = customer.Id;
            CustomerDisplayName = $"{customer.FirstName} {customer.LastName}";
        }

        public Guid CustomerId { get; set; }

        public string CustomerDisplayName { get; set; }
    }
}
