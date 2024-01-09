using Microsoft.EntityFrameworkCore;
using Banking.Services;
using Banking.Models;
using System.Net;

namespace Banking.Services
{
    public class TransactionService(Context context) : ITransactionService
    {
        UserTransactionModel? ITransactionService.CreateTransaction(UserTransactionModel transaction)
        {
            if (transaction.SenderId == null) return null;

            /// new HttpStatusCodeResult(HttpStatusCode.BadRequest, "cant send to self")
            if (transaction.SenderId == transaction.RecieverId) return null;

            context.Database.BeginTransaction();

            context.Add(transaction);

            ///update statement also updates user's last activity
            int modifiedCount1 = context.Database.ExecuteSql($"UPDATE user SET balance = balance + {transaction.Amount} WHERE id = {transaction.RecieverId}");
            int modifiedCount2 = context.Database.ExecuteSql($"UPDATE user SET balance = balance - {transaction.Amount} WHERE id = {transaction.SenderId}");

            UserModel sender = context.Users.Where(user => user.Id == transaction.SenderId).First();
            UserModel reciever = context.Users.Where(user => user.Id == transaction.RecieverId).First();

            if (sender.Balance < 0)
            {
                /// Balance cannot go lower than 0
                context.Database.RollbackTransaction();
                return null;
            }

            /// Uses raw sql query so "increment" can be used in order to prevent race conditions
            int modifiedCount3 = context.Database.ExecuteSql($"UPDATE bank SET TotalBalance = TotalBalance + {transaction.Amount} WHERE id = {reciever.BankId};");
            int modifiedCount4 = context.Database.ExecuteSql($"UPDATE bank SET TotalBalance = TotalBalance - {transaction.Amount} WHERE id = {sender.BankId};");
            int modifiedCount5 = context.Database.ExecuteSql($"UPDATE bank SET TotalDeposit = TotalDeposit + {transaction.Amount} WHERE id = {reciever.BankId};");
            int modifiedCount6 = context.Database.ExecuteSql($"UPDATE bank SET TotalWithdrawl = TotalWithdrawl - {transaction.Amount} WHERE id = {sender.BankId};");

            if (modifiedCount1 == 0 || modifiedCount2 == 0 || modifiedCount3 == 0 || modifiedCount4 == 0 || modifiedCount5 == 0 || modifiedCount6 == 0)
            {
                /// One of the Id did not match
                context.Database.RollbackTransaction();
                return null;
            }

            /// Everything is good
            context.Database.CommitTransaction();

            return transaction;
        }


        /// <summary>
        /// only to be used for seeding (Error prone)
        /// </summary>
        UserTransactionModel ITransactionService.SignInDeposit(UserTransactionModel transaction)
        {
            context.Database.BeginTransaction();

            context.Add(transaction);
            UserModel reciever = context.Users.Where(user => user.Id == transaction.RecieverId).First();

            ///update statement also updates user's last activity
            context.Database.ExecuteSql($"UPDATE user SET balance = balance + {transaction.Amount} WHERE id = {transaction.RecieverId}");

            /// Uses raw sql query so "increment" can be used in order to prevent race conditions
            context.Database.ExecuteSql($"UPDATE bank SET TotalBalance = TotalBalance + {transaction.Amount} WHERE id = {reciever.BankId};");
            context.Database.ExecuteSql($"UPDATE bank SET TotalDeposit = TotalDeposit + {transaction.Amount} WHERE id = {reciever.BankId};");

            context.Database.CommitTransaction();

            return transaction;
        }

        List<UserTransactionModel> ITransactionService.GetUserTransaction(string userId, DateTime start, DateTime end)
        {
            return context.Transactions
             .Where(t => t.RecieverId == userId || t.SenderId == userId)
             .Where(t => t.CreatedAt >= start && t.CreatedAt <= end)
             .Include(t => t.Reciever)
               .ThenInclude(r => r.Bank)
             .OrderBy(t => t.CreatedAt)
             .ToList();
        }

        List<UserTransactionModel> ITransactionService.GetBankTransaction(int bankId, DateTime start, DateTime end)
        {
            return [
                .. context.Transactions
                .Where(transaction => transaction.CreatedAt >= start)
                .Where(transaction => transaction.CreatedAt <= end)
                .Include(transaction => transaction.Reciever)
                .ThenInclude(reciever => reciever!.Bank)
                .Include(transaction => transaction.Sender)
                .ThenInclude(sender => sender!.Bank)
                .Where(transaction => transaction.Reciever!.BankId == bankId || transaction.Sender!.BankId == bankId)
                .OrderBy(transaction => transaction.CreatedAt)
                .ToList()
                ];
        }
    }
}
