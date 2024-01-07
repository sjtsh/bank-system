using Banking.Services;

namespace Banking.Models.Seeder
{
    public class BankSeeder(IBankService service)
    {
        private readonly string[] BankNames = ["The National Bank", "Agricultural Bank", "Trust Source Bank", "Native Non Profitable Bank", "Standard Chartered Bank"];

        void Seed() {
            if(service.GetBanks().Count == 0)
            {
                service.CreateBanks(BankNames);
            }
        }
    }
}
  