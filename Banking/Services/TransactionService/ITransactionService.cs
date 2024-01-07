using Banking.Models;
    
namespace Banking.Services
{
    internal interface ITransactionService
    {
        /// <summary>
        /// Create transaction 
        /// 1. increases reciever's total balance
        /// 2. decreases sender's total balance
        /// 3. creates a new transaction
        /// 4. increases receiving bank's total balance
        /// 5  increases receiving bank's total deposit
        /// 6. decreases sending bank's total balance
        /// 7. decreases sending bank's total withdrawl
        /// 8. update user's last activity
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
