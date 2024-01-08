using Microsoft.EntityFrameworkCore;
using Banking.Models;
using Banking.Models.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Banking.Services
{
    public class Context(DbContextOptions options) : IdentityDbContext<UserModel>(options){

        public DbSet<BankModel> Banks { get; set; }
        public DbSet<UserTransactionModel> Transactions { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new BankConfiguration());
            builder.ApplyConfiguration(new UserTransactionConfiguration());
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}