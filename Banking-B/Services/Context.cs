using Microsoft.EntityFrameworkCore;
using Banking_B.Models;

namespace Banking_B.Services
{
    public class Context(DbContextOptions options) : DbContext(options){

        public DbSet<BankModel> Banks { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<UserTransactionModel> Transactions { get; set; }

        public static Context Get()
        {
            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlite($@"Data Source=D:\Study\Projects\bank-system\Banking-B\mydb.db;Version=3;");
            return new Context(optionsBuilder.Options);
        }
    }
}