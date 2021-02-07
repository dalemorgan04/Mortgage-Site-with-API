using System;

namespace MortgageApi.ViewModels.SearchNS
{
    public class SearchParametersViewModel
    {
        public Guid CustomerId { get; set; }
        public decimal PropertyValue { get; set; }
        public decimal Deposit { get; set; }
        public int MortgageType { get; set; }
    }
}