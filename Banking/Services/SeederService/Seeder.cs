using Banking.Models;
using Banking.Models.Seeder;
using Microsoft.AspNetCore.Identity;

namespace Banking.Services
{
    public class Seeder(RoleManager<IdentityRole> roleManager, UserManager<UserModel> userManager, IUserService userService, IBankService bankService, ITransactionService transactionService, Context context) : ISeeder
    {
          
        private readonly RoleManager<IdentityRole> RoleManager = roleManager;
        private readonly UserManager<UserModel> UserManager = userManager;
        private readonly Context Context = context;
        private readonly IUserService UserService = userService;
        private readonly IBankService BankService = bankService;
        private readonly ITransactionService TransactionService = transactionService;

        async Task ISeeder.SeedIfEmpty()
        {
            Context.Database.EnsureCreated();
            new BankSeeder(BankService).Seed();
            await new UserSeeder(RoleManager, UserManager, UserService, TransactionService).Seed();
        }
    }
}