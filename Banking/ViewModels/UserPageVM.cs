using Banking.Models;

namespace Banking.ViewModels
{
    public class UserPageVM
    {
        public UserModel? UserModel { get; set; }
        public List<UserModel>? Users { get; set; }
        public List<BankModel>? Banks { get; set; }
        public List<UserTransactionModel>? TransactionModels { get; set; }
        public TransactionVM? TransactionVM { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? SenderId { get; set; }
    }
}
