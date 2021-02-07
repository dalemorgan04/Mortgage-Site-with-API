using MortgageApi.Models;
using MortgageApi.ViewModels.MortgageNS;
using System;

namespace MortgageApi.ViewModels.SearchNS
{
    public class SearchDetailsViewModel
    {
        public SearchDetailsViewModel(Mortgage mortgage, int searchId)
        {
            Mortgage = new MortgageViewModel(mortgage);
            SearchId = searchId;
        }

        public readonly MortgageViewModel Mortgage;
        public readonly int SearchId;
    }
}