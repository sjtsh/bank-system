namespace Banking.ViewModels
{
    public class TransactionVM
    {
        public string? SenderId { get; set; }
        public string? ReciverId { get; set; }
        public int Amount { get; set; }
        public string? Remarks { get; set; }
    }
}
