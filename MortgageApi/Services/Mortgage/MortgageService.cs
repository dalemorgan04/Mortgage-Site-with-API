using Microsoft.EntityFrameworkCore;
using MortgageApi.Data;
using MortgageApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MortgageApi.Services.MortgageNS
{
    public class MortgageService : IMortgageService
    {
        private readonly DataContext _context;
        public MortgageService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Mortgage>> GetMortgages()
        {
            return await _context.Mortgages
                .Include( m => m.Bank)
                .ToListAsync();
        }

        public async Task<Mortgage> GetMortgage(int mortgageId)
        {
            return await _context.Mortgages.Include(m => m.Bank).FirstOrDefaultAsync(m => m.Id == mortgageId);
        }
    }
}
