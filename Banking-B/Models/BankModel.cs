namespace Banking_B.Models
{
    public class BankModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TotalBalance { get; set; }
        public int TotalWithdrawl { get; set; }
        public int TotalDeposit { get; set; }

        public List<UserModel> Users { get; set; } 
        public List<UserTransactionModel> RecievedTransactions { get; set; }
        public List<UserTransactionModel> SentTransactions { get; set; }
    }
}
 