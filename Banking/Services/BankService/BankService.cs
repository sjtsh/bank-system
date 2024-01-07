using Banking.Models;

namespace Banking.Services
{
    public class BankService : IBankService
    {

        int IBankService.CreateBanks(string[] names)
        {
            using Context context = Context.Get();
            {
                context.AddRange(names.Select(name => new BankModel(name)));
                int count = context.SaveChanges();
                return count;
            }
        }

        List<BankModel> IBankService.GetBanks()
        {
            using Context context = Context.Get();
            {
                return [.. context.Banks.OrderBy(bank => bank.Name)];

            }
        }
    }
}
