using Microsoft.EntityFrameworkCore;
using Banking_B.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Banking_B.Services
{
    public class SqlConnector : IDataConnection
    {

        UserModel IDataConnection.FindUserByPhone(string phone)
        {
            using Context context = Context.Get();
            return context.Users.Where(user => user.PhoneNumber == phone && !user.IsDeleted).FirstOrDefault();
        }

        UserModel IDataConnection.CreateUser(UserModel user)
        {
            using Context context = Context.Get();
            context.Add(user);
            return user;
        }

        List<UserModel> IDataConnection.GetUsers()
        {
            using Context context = Context.Get();
            return context.Users.Where(user => !user.IsDeleted).Include(user => user.Bank).ToList();
        }

        UserModel IDataConnection.UpdateUser(UserModel information)
        {
            using Context context = Context.Get();
            UserModel user = context.Users.Where(element => information.Id == element.Id).FirstOrDefault();
            if (user.Id == null)
            {
                return new UserModel();
            }
            user.FirstName = information.FirstName;
            user.MiddleName = information.MiddleName;
            user.LastName = information.LastName;
            user.Email = information.Email;
            user.DOB = information.DOB;
            user.IsAdmin = information.IsAdmin;
            user.IsDeleted = information.IsDeleted;
            return user;
        }

        List<BankModel> IDataConnection.GetBanks()
        {
            using Context context = Context.Get();
            {
                return context.Banks.OrderBy(bank => bank.Name).ToList();
            }
        }

        UserTransactionModel IDataConnection.CreateTransaction(UserTransactionModel transaction)
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

        List<UserTransactionModel> IDataConnection.GetUserTransaction(int userId, DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        List<UserTransactionModel> IDataConnection.GetBankTransaction(int bankId, DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        List<UserTransactionModel> IDataConnection.GetTransactions(int bankId, DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        void IDataConnection.Initialize()
        {
            CreateDatabaseTablesIfNotExist();
        }

        private void CreateDatabaseTablesIfNotExist()
        {
            ///
            /// try
            /// {
            /// // Connection.Create€
            ///     var databaseCreator = (Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator);
            ///     databaseCreator.CreateTables();
            /// }
            ///catch (System.Data.SqlClient.SqlException)
            ///{
            /// //A SqlException will be thrown if tables already exist. So simply ignore it.
            ///}  
            ///
        }
    }
}
