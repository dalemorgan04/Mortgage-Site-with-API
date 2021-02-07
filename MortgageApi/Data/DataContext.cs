using Microsoft.EntityFrameworkCore;
using MortgageApi.Models;

namespace MortgageApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SearchMortgage>().HasKey(sm => new { sm.SearchId, sm.MortgageId });
            modelBuilder.Entity<SearchMortgage>()
                .HasOne(sm => sm.Search)
                .WithMany(sm => sm.MortgagesOfInterest)
                .HasForeignKey(sm => sm.SearchId);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Mortgage> Mortgages { get; set; }
        public DbSet<Search> Searches { get; set; }
        public DbSet<SearchMortgage> SearchMortgages { get; set; }
    }
}