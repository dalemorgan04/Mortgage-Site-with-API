using Microsoft.EntityFrameworkCore;
using MortgageApi.Data;
using MortgageApi.Models;
using MortgageApi.Models.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MortgageApi.Services.SearchNS
{
    public class SearchService : ISearchService
    {
        private readonly DataContext _context;

        public SearchService(DataContext context)
        {
            _context = context;
        }

        public async Task<int> CreateSearch(Search search)
        {
            //This is for the purpose of tracking a customers searches
            //The data can be valuable in reporting
            _context.Searches.Add(search);
            await _context.SaveChangesAsync();
            return search.Id;
        }

        public async Task<Search> GetSearch(int searchId)
        {
            //This is for the purpose of tracking a customers searches
            //The data can be valuable in reporting
            return await _context.Searches.Include(s => s.Customer).FirstOrDefaultAsync(s => s.Id == searchId);
        }

        public async Task<List<Mortgage>> FindMortgages(Search search, bool saveSearch = false)
        {
            if (saveSearch)
            {
                _context.Searches.Add(search);
                await _context.SaveChangesAsync();
            }

            return await _context.Mortgages
                .Include(m => m.Bank)
                .Where(m =>
                   (m.LoanToValueMax / 100 >= search.LoanToValue) &&
                   (search.MortgageType == MortgageTypeIdentifier.None || m.MortgageType == search.MortgageType))
                .ToListAsync();
        }

        public async Task<Mortgage> RegisterMortgageOfInterest(int searchId, int mortgageId)
        {
            //Add a mortgage that the user has clicked on within a particular search
            var searchMortgageExists = await _context.SearchMortgages.Where(sm => sm.SearchId == searchId && sm.MortgageId == mortgageId).AnyAsync();
            var mortgage = await _context.Mortgages.Include(m => m.Bank).Where(m => m.Id == mortgageId).FirstOrDefaultAsync();
            if (!searchMortgageExists)
            {
                var search = await _context.Searches.FindAsync(searchId);
                var searchMortgage = new SearchMortgage()
                {
                    Mortgage = mortgage,
                    MortgageId = mortgage.Id,
                    Search = search,
                    SearchId = search.Id
                };
                _context.SearchMortgages.Add(searchMortgage);
                await _context.SaveChangesAsync();
            }
            return mortgage;
        }
    }
}