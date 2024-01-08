using Banking.Services;
using JWTAuthentication.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Banking.Models.Seeder
{
    public class UserSeeder(RoleManager<IdentityRole> roleManager, UserManager<UserModel> userManager, IUserService service, ITransactionService transactionService)
    {
        private readonly UserModel[] AdminUsers = [
            new UserModel("9876543210", "Admin", "Admin", "Admin@123"),
            new UserModel("9840339289", "Manjil", "Shrestha", "Admin@123"),
        ];

        private readonly UserModel[] NormalUsers = [
            new UserModel("9876543220", "Ram", "Louise", "Shrestha", "ram@gmail.com", "User@123", 1),
            new UserModel("9840339239", "Shyam", null, "Jones", "shyam@gmail.com", "User@123", 2),
            new UserModel("9876541220", "Sita", "Grace", "Clarke", "sita@gmail.com", "User@123", 3),
            new UserModel("9840349239", "Radha", null, "Smith", "radha@gmail.com", "User@123", 4), 
            new UserModel("9876553220", "Suraj", null, "Baker", "suraj@gmail.com", "User@123", 5),
            new UserModel("9840330239", "Hari", null, "Walker", "hari@gmail.com", "User@123", 1),
            new UserModel("9876543120", "Priya", "Rose", "Adam", "priya@gmail.com", "User@123", 2),
            new UserModel("9830339239", "Gita", null, "Brown", "gita@gmail.com", "User@123", 3),
        ];

        public async Task Seed()
        {
            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            if (service.GetUsers().Count > 0) return;
            for (int i = 0; i < AdminUsers.Length; i++)
            {
                IdentityResult identity = await userManager.CreateAsync(AdminUsers[i], AdminUsers[i].Password);
                await userManager.AddToRolesAsync(AdminUsers[i], [UserRoles.Admin, UserRoles.User]);
            }
            for (int i = 0; i < NormalUsers.Length; i++)
            {
                await userManager.CreateAsync(NormalUsers[i], NormalUsers[i].Password);
                await userManager.AddToRoleAsync(NormalUsers[i], UserRoles.User);
                transactionService.SignInDeposit(new UserTransactionModel(NormalUsers[i].Id, 10000));
            }
        }
    }
}
