using MortgageApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MortgageApi.Services.MortgageNS
{
    public interface IMortgageService
    {
        public Task<List<Mortgage>> GetMortgages();

        public Task<Mortgage> GetMortgage(int mortgageId);
    }
}