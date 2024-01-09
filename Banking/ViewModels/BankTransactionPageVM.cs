using Banking.Models;

namespace Banking.ViewModels;

public class BankTransactionPageVM
{
    public List<UserTransactionModel>? TransactionModels { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int? BankId { get; set; }
}
