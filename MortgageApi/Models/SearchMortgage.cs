using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MortgageApi.Models
{
    public class SearchMortgage
    {
        public int SearchId { get; set; }
        public Search Search { get; set; }
        public int MortgageId { get; set; }
        public Mortgage Mortgage { get; set; }

    }
}
