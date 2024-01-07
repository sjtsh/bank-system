using Banking_B.Models;

namespace Banking_B.Services
{
    internal interface IBankService
    {
        /// <summary>
        /// Get Bank Summary
        /// Sorted by name
        /// </summary>
        /// <returns>Bank summary</returns>
        List<BankModel> GetBanks();

        /// <summary>
        /// Creates many banks
        /// Only for seeding
        /// </summary>
        /// <returns>Number of banks created</returns>
        int CreateBanks(string[] names);
    }
}