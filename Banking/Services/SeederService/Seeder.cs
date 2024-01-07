using Microsoft.EntityFrameworkCore.Storage;
using Banking.Models;
using Banking.Models.Seeder;
using Microsoft.AspNetCore.Identity;

namespace Banking.Services
{
    public class Seeder(RoleManager<IdentityRole> roleManager, UserManager<UserModel> userManager, IUserService userService, IBankService bankService): ISeeder
    {
          
        private readonly RoleManager<IdentityRole> RoleManager = roleManager;
        private readonly UserManager<UserModel> UserManager = userManager;
        private readonly IUserService UserService = userService;
        private readonly IBankService BankService = bankService;

        void ISeeder.SeedIfEmpty()
        {
            _ = new BankSeeder(BankService);
            _ = new UserSeeder(RoleManager, UserManager, UserService);
        }
    }
}