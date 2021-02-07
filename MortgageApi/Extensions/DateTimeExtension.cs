using System;

namespace MortgageApi.Extensions
{
    public static class DateTimeExtensions
    {
        public static int GetAge(this DateTime birthdate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthdate.Year;

            // Go back to the year in which the person was born in case of a leap year
            if (birthdate.Date > today.AddYears(-age)) age--;

            return age;
        }
    }
}