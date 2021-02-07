using MortgageApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MortgageApi.Services.SearchNS
{
    public interface ISearchService
    {
        public Task<int> CreateSearch(Search search);
        public Task<Search> GetSearch(int searchId);
        public Task<List<Mortgage>> FindMortgages(Search search, bool saveSearch = false);
        public Task<Mortgage> RegisterMortgageOfInterest(int searchId, int mortgageId);
    }
}
