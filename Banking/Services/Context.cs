using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Banking.Models;
using Banking.Models.Configuration;
using Banking.Models.Seeder;
using Microsoft.AspNetCore.Identity;

namespace Banking.Services
{
    public class Context(DbContextOptions options) : DbContext(options){

        public DbSet<BankModel> Banks { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<UserTransactionModel> Transactions { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new BankConfiguration());
            builder.ApplyConfiguration(new UserTransactionConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            CreateTables().OnConfiguring(optionsBuilder);
        }

        private Context CreateTables()
        {
            try
            {
                RelationalDatabaseCreator? databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                databaseCreator?.CreateTables();
            }
            catch (Exception)
            {
                //A SqlException will be thrown if tables already exist.
            }
            return this;
        }
    }
}