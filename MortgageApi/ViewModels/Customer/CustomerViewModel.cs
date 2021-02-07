using MortgageApi.Models;
using System;

namespace MortgageApi.ViewModels.CustomerNS
{
    public class CustomerViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DOB { get; set; }

        public string Email { get; set; }
    }
}