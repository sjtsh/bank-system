using Microsoft.EntityFrameworkCore;
using Banking_B.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Xml.Linq;

namespace Banking_B.Services
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
