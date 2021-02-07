using MortgageApi.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MortgageApi.Models
{
    public class Search
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public Customer Customer { get; set; }

        [Required]
        public decimal PropertyValue { get; set; }

        [Required]
        public decimal Deposit { get; set; }

        [Required]
        public MortgageTypeIdentifier MortgageType { get; set; }

        public decimal LoanToValue
        {
            get
            {

                return (decimal)(PropertyValue - Deposit)/PropertyValue;

            }
        }

        public IEnumerable<SearchMortgage> MortgagesOfInterest { get; set; }
    }
}