using MortgageApi.Models.Enums;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;

namespace MortgageApi.Api.Entities
{
    public class SearchRequest
    {
        [JsonProperty("customer_id")]
        public Guid CustomerId { get; set; }

        [JsonProperty("property_value")]
        public decimal PropertyValue { get; set; }

        [JsonProperty("deposit")]
        public decimal Deposit { get; set; }

        [JsonProperty("mortgage_type")]
        public int MortgageTypeInt { get; set; }

        public MortgageTypeIdentifier MortgageType => (MortgageTypeIdentifier)MortgageTypeInt;

        public bool IsValid => String.IsNullOrEmpty(GetValidationMessage);

        public string GetValidationMessage
        {
            get
            {
                var sb = new StringBuilder();
                if (PropertyValue <= 0 || PropertyValue > 9999999) sb.Append("Property Value not valid; ");
                if (Deposit < 0 || Deposit > 9999999)sb.Append("Deposit not valid; ");
                if (LoanToValue > 0.9M || LoanToValue <= 0M) sb.Append("Your loan to value ratio disqualifies you; ");
                return sb.ToString();
            }
        }

        public decimal LoanToValue { get
            {
                if (PropertyValue <= 0)
                {
                    return 0M;
                }
                else
                {
                    return (decimal)(PropertyValue - Deposit) / PropertyValue;
                }
            } }
    }
}