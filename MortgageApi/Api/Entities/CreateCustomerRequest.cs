using MortgageApi.Extensions;
using Newtonsoft.Json;
using System;
using System.Text;

namespace MortgageApi.Api.Entities
{
    public class CreateCustomerRequest
    {
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("dob")]
        public DateTime DOB { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        public bool IsValid => String.IsNullOrEmpty(GetValidationMessage);

        public string GetValidationMessage
        {
            get
            {
                var sb = new StringBuilder();
                if (String.IsNullOrEmpty(FirstName)) sb.Append("First Name missing; ");
                if (String.IsNullOrEmpty(LastName)) sb.Append("Last Name missing; ");
                if (!Email.IsEmail()) sb.Append("Email is invalid; ");
                if (DOB == DateTime.MinValue || DOB.GetAge() < 18) sb.Append("DOB Must be over 18; ");
                return sb.ToString();
            }
        }
    }
}