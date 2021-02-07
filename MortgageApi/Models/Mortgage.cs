using Microsoft.EntityFrameworkCore;
using MortgageApi.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MortgageApi.Models
{
    [Index(nameof(LoanToValueMax), nameof(MortgageType), nameof(InterestRate))]
    public class Mortgage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public Bank Bank { get; set; }

        [Required]
        public decimal InterestRate { get; set; }

        [Required]
        public MortgageTypeIdentifier MortgageType { get; set; }

        [Required]
        public decimal LoanToValueMax { get; set; }
    }
}