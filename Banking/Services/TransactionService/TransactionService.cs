using Microsoft.EntityFrameworkCore;
using Banking.Services;
using Banking.Models;

namespace Banking.Services
{
    public class TransactionService : ITransactionService
    {
        UserTransactionModel ITransactionService.CreateTransaction(UserTransactionModel transaction)
        {
            /// Uses raw sql query so "increment can be used" in order to prevent race conditions
            using Context context = Context.Get();
            {
                context.Add(transaction);
                context.Database.BeginTransaction();
                context.Database.ExecuteSqlRaw($"UPDATE [Blogs] SET [Url] = NULL");
                return transaction;
            }
        }

        /// <summary>
        /// 1. increases reciever's total balance
        /// 2. creates a new transaction
        /// 3. increases receiving bank's total balance
        /// 4. increases receiving bank's total deposit
        /// </summary>
        private UserTransactionModel CreateTransaction(UserTransactionModel transaction)
        {
            using Context context = Context.Get();
            {
                context.Database.ExecuteSqlRaw($"UPDATE [Blogs] SET [Url] = NULL");
                return transaction;
            }
        }

        List<UserTransactionModel> ITransactionService.GetUserTransaction(int userId, DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        List<UserTransactionModel> ITransactionService.GetBankTransaction(int bankId, DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        List<UserTransactionModel> ITransactionService.GetTransactions(int bankId, DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
