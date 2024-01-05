using Microsoft.EntityFrameworkCore;
using Banking_B.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Banking_B.Services
{
    public class Context(DbContextOptions options) : DbContext(options){

        public DbSet<BankModel> Banks { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<UserTransactionModel> Transactions { get; set; }


        private Context CreateTables()
        {
            try
            {
                Console.WriteLine("Creating tables");
                RelationalDatabaseCreator? databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                databaseCreator?.CreateTables();
                Console.WriteLine("Created tables");
            }
            catch (Exception e)
            {
                Console.WriteLine("Throwing exception");
                //A SqlException will be thrown if tables already exist.
            }
            return this;
        }

        public static Context Get()
        {
            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlite($@"Data Source=D:\Study\Projects\bank-system\Banking-B\mydb.db;");
            return new Context(optionsBuilder.Options).CreateTables();
        }
    }
}