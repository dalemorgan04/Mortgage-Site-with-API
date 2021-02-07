using MortgageApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MortgageApi.ViewModels.MortgageNS
{
    public class MortgagesViewModel
    {
        public MortgagesViewModel(List<Mortgage> mortgages, Guid customerId)
        {
            Mortgages = mortgages.Select(m => new MortgageViewModel(m)).OrderBy( m => m.InterestRate).ToList();
            CustomerId = customerId;
        }

        public MortgagesViewModel(IEnumerable<Mortgage> mortgages)
        {
            Mortgages = mortgages.Select(m => new MortgageViewModel(m));
        }

        public readonly IEnumerable<MortgageViewModel> Mortgages;
        public readonly Guid CustomerId;
    }
}