using Banking.Models;
    
namespace Banking.Services
{
    public interface ITransactionService
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
        UserTransactionModel? CreateTransaction(UserTransactionModel transaction);

        /// <summary>
        /// Sign in deposit for new users to play with
        /// 1. increases reciever's total balance
        /// 2. creates a new transaction
        /// 3. increases receiving bank's total balance
        /// 4  increases receiving bank's total deposit
        /// 5. update user's last activity
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns>the created transaction</returns>
        UserTransactionModel SignInDeposit(UserTransactionModel transaction);

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
        List<UserTransactionModel> GetUserTransaction(string userId, DateTime start, DateTime end);


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
        /// 3. the sending 
        /// 4. the recieving bank
        /// 5. the sending bank
        /// </returns>
        List<UserTransactionModel> GetBankTransaction(int bankId, DateTime start, DateTime end);

    }
}
