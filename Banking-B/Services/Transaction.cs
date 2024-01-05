using Microsoft.EntityFrameworkCore;
using Banking_B.Models;

namespace Banking_B.Services
{
    public class Transaction : DbContext
    {

        public DbSet<BankModel> Banks { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<UserTransactionModel> Transactions { get; set; }

        public Transaction() : base("name=MyDbConnection")
        {
        }
    }
}