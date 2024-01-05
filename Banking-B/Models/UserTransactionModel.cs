namespace Banking_B.Models
{
    public class UserTransactionModel
    {
        public int Id { get; set; }

        public string Remark { get; set; }

        public int ClosingBalance { get; set; }

        public bool IsDebit { get; set; }

        public bool IsInterBank { get; set; }

        public UserModel Reciever { get; set; }

        public UserModel Sender { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}
