using MortgageApi.Models;
using MortgageApi.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MortgageApi.Data
{
    public class DbInitializer
    {
        public static void Initialize(DataContext context)
        {
            context.Database.EnsureCreated();

            if (context.Customers.Any())
            {
                return; //DB already seeded
            }

            //Customer
            var customer = new Customer { Id = Guid.NewGuid(), FirstName = "John", LastName = "Smith", DOB = DateTime.Parse("2000-01-01"), Email = "johnsmith@email.com" };
            context.Customers.Add(customer);
            context.SaveChanges();

            //Bank
            var banks = new Bank[]
            {
                new Bank(){ Name="Bank A"},
                new Bank(){ Name="Bank B"},
                new Bank(){ Name="Bank C"},
                new Bank(){ Name="Bank D"},
                new Bank(){ Name="Bank E"}
            };
            foreach (Bank bank in banks)
            {
                context.Banks.Add(bank);
            };
            context.SaveChanges();

            //Mortgages
            var products = new List<Mortgage>();
            var seededBanks = context.Banks.ToList();
            foreach (Bank bank in seededBanks)
            {
                for (int interest = 1; interest < 11; interest++)
                {
                    for (int loanToValue = 1; loanToValue < 10; loanToValue++)
                    {
                        var mortgageTpes = new MortgageTypeIdentifier[] { MortgageTypeIdentifier.Variable, MortgageTypeIdentifier.Fixed };
                        foreach (MortgageTypeIdentifier mortgageType in mortgageTpes)
                        {
                            var random = new Random();
                            products.Add(new Mortgage()
                            {
                                Bank = bank,
                                InterestRate = (decimal)interest + (decimal)random.Next(1,99)/100M,
                                LoanToValueMax = loanToValue * 10,
                                MortgageType = mortgageType
                            });
                        }
                    }
                }
            }
            foreach (Mortgage product in products)
            {
                context.Mortgages.Add(product);
            };
            context.SaveChanges();
        }
    }
}