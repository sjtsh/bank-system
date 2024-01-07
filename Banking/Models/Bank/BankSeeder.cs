using Banking.Services;

namespace Banking.Models.Seeder
{
    public class BankSeeder
    {
        private readonly IBankService service = new BankService();
        private readonly string[] BankNames = ["The National Bank", "Agricultural Bank", "Trust Source Bank", "Native Non Profitable Bank", "Standard Chartered Bank"];

        public BankSeeder() {
            service.CreateBanks(BankNames);
        }
    }
}
  