using MortgageApi.Extensions;
using MortgageApi.Models;
using MortgageApi.Models.Enums;

namespace MortgageApi.ViewModels.MortgageNS
{
    public class MortgageViewModel
    {
        public MortgageViewModel(Mortgage mortgage)
        {
            Id = mortgage.Id;
            BankId = mortgage.Bank.Id;
            BankName = mortgage.Bank.Name;
            InterestRate = mortgage.InterestRate;
            LoanToValueMax = mortgage.LoanToValueMax;
            MortgageType = mortgage.MortgageType;
            MortgageTypeName = mortgage.MortgageType.GetDescription();
        }

        public readonly int Id;
        public readonly int BankId;
        public readonly string BankName;
        public readonly decimal InterestRate;
        public readonly MortgageTypeIdentifier MortgageType;
        public readonly string MortgageTypeName;
        public readonly decimal LoanToValueMax;
    }
}