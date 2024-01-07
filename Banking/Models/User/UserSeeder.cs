using Banking.Services;
using JWTAuthentication.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Banking.Models.Seeder
{
    public class UserSeeder(RoleManager<IdentityRole> roleManager, UserManager<UserModel> userManager, IUserService service)
    {
        private readonly UserModel[] AdminUsers = [
            new UserModel("9876543210", "Admin", "Admin", "admin123"),
            new UserModel("9840339289", "Manjil", "Shrestha", "admin123"),
        ];

        private readonly UserModel[] NormalUsers = [
            new UserModel("9876543220", "Ram", "Louise", "Shrestha", "ram@gmail.com", "user1234"),
            new UserModel("9840339239", "Shyam", null, "Jones", "shyam@gmail.com", "user1234"),
            new UserModel("9876541220", "Sita", "Grace", "Clarke", "ram@gmail.com", "user1234"),
            new UserModel("9840349239", "Radha", null, "Smith", "shyam@gmail.com", "user1234"),
            new UserModel("9876553220", "Suraj", null, "Baker", "ram@gmail.com", "user1234"),
            new UserModel("9840330239", "Hari", null, "Walker", "shyam@gmail.com", "user1234"),
            new UserModel("9876543120", "Priya", "Rose", "Adam", "ram@gmail.com", "user1234"),
            new UserModel("9830339239", "Gita", null, "Brown", "shyam@gmail.com", "user1234"),
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
                UserModel user = service.CreateUser(AdminUsers[i]);
                await userManager.AddToRolesAsync(user, [UserRoles.Admin, UserRoles.User]);

            }
            for (int i = 0; i < NormalUsers.Length; i++)
            {
                UserModel user = service.CreateUser(NormalUsers[i]);
                await userManager.AddToRoleAsync(user, UserRoles.User);
            }
        }
    }
}
