using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Banking.Models;
using Banking.Models.Configuration;
using Banking.Models.Seeder;

namespace Banking.Services
{
    internal class Context(DbContextOptions options) : DbContext(options){

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

        private Context CreateTables()
        {
            try
            {
                RelationalDatabaseCreator? databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                databaseCreator?.CreateTables();
                SeedIfEmpty();
            }
            catch (Exception)
            {
                //A SqlException will be thrown if tables already exist.
            }
            return this;
        }
 
        private static void SeedIfEmpty()
        {
            _ = new BankSeeder();
            _ = new UserSeeder();
        }

        public static Context Get()
        {
            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlite($@"Data Source=.\mydb.db;");
            return new Context(optionsBuilder.Options).CreateTables();
        }
    }
}