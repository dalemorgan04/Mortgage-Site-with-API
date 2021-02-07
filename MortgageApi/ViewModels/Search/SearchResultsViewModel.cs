using MortgageApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MortgageApi.ViewModels.MortgageNS
{
    public class SearchResultsViewModel
    {
        public SearchResultsViewModel(Search search, List<Mortgage> mortgages)
        {
            SearchId = search.Id;
            Mortgages = mortgages.Select(m => new MortgageViewModel(m)).OrderBy( m => m.InterestRate).ToList();
            CustomerId = search.Customer.Id;
            LoanToValue = search.LoanToValue;
            PropertyValue = search.PropertyValue;
            Deposit = search.Deposit;
        }

        public SearchResultsViewModel(IEnumerable<Mortgage> mortgages)
        {
            Mortgages = mortgages.Select(m => new MortgageViewModel(m));
        }

        public readonly int SearchId;
        public readonly Guid CustomerId;
        public readonly IEnumerable<MortgageViewModel> Mortgages;
        public readonly decimal LoanToValue;
        public readonly decimal PropertyValue;
        public readonly decimal Deposit;
    }
}