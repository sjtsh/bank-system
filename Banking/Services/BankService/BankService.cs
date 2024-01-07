using Banking.Models;
using Microsoft.EntityFrameworkCore;

namespace Banking.Services
{
    public class BankService(Context context) : IBankService
    {
        int IBankService.CreateBanks(string[] names)
        {
            context.AddRange(names.Select(name => new BankModel(name)));
            int count = context.SaveChanges();
            return count;
        }

        List<BankModel> IBankService.GetBanks()
        {
            return [.. context.Banks.OrderBy(bank => bank.Name)];
        }
    }
}
