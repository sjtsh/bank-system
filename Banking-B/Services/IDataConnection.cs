using Banking_B.Models;

namespace Banking_B.Services
{
    internal interface IDataConnection
    {

        /// <summary>
        /// 1. Creates the database tables it does not already exist
        /// </summary>
        void Initialize();

        /// <summary>   
        /// Get Users
        /// That is not deleted
        /// Sorted by recent activity
        /// </summary>
        /// <returns>
        /// 1. Users
        /// 2. their Bank
        /// </returns>
        List<UserModel> GetUsers();

        /// <summary>
        /// Find user summary
        /// Might not return the user if phone not in use
        /// Wont return if the user has been deleted
        /// </summary>
        /// <param name="phone"></param>
        /// <returns>Maybe User</returns>
        UserModel FindUserByPhone(string phone);

        /// <summary>
        /// Creates user
        /// 1. Creates a £10000 sign-in bonus deposit
        /// </summary>
        /// <param name="information"></param>
        /// <returns>the created user</returns>
        UserModel CreateUser(UserModel information);

        /// <summary>
        /// Updates user
        /// Allowed by the admin or the user himself
        /// </summary>
        /// <param name="information"></param>
        /// <returns>the updated user</returns>
        UserModel UpdateUser(UserModel information);

        /// <summary>
        /// Get Bank Summary
        /// Sorted by name
        /// </summary>
        /// <returns>Bank summary</returns>
        List<BankModel> GetBanks();


        /// <summary>
        /// Create transaction 
        /// 1. increases reciever's total balance
        /// 2. decreases sender's total balance
        /// 3. creates a new transaction
        /// 4. increases receiving bank's total balance
        /// 5  increases receiving bank's total deposit
        /// 6. decreases sending bank's total balance
        /// 7. decreases sending bank's total withdrawl
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns>the created transaction</returns>
        UserTransactionModel CreateTransaction(UserTransactionModel transaction);

        /// <summary>
        /// Get the user's transactions
        /// Sorted by creation date
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns>
        /// 1. user's transactions
        /// 2. the recieving user
        /// 3. recieving bank
        /// </returns>
        List<UserTransactionModel> GetUserTransaction(int userId, DateTime start, DateTime end);


        /// <summary>
        /// Get the bank's transactions
        /// Sorted by creation date
        /// </summary>
        /// <param name="bankId"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns> 
        /// 1. bank's transactions
        /// 2. the recieving user
        /// 3. the sending user
        /// </returns>
        List<UserTransactionModel> GetBankTransaction(int bankId, DateTime start, DateTime end);

        /// <summary>
        /// Get all transactions
        /// Sorted by creation date
        /// </summary>
        /// <param name="bankId"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns> 
        /// 1. bank's transactions
        /// 2. their recieving user
        /// 3. the sending user
        /// 4. the sending bank 
        /// 5. the recieving bank
        /// </returns>
        List<UserTransactionModel> GetTransactions(int bankId, DateTime start, DateTime end);
    }
}
