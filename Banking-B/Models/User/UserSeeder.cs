using Banking_B.Services;

namespace Banking_B.Models.Seeder
{
    public class UserSeeder
    {
        private readonly IUserService service = new UserService();
        private readonly UserModel[] AdminUsers = [
            new UserModel("9876543210", "admin", "shrestha", "admin123"),
            new UserModel("9840339289", "manjil", "shrestha", "admin123"),
        ];

        private readonly UserModel[] NormalUsers = [
            new UserModel("9876543210", "manjil", null, "shrestha","admin@gmail.com", "admin123"),
            new UserModel("9840339289", "manjil", "shrestha", "admin123"),
        ];

        public UserSeeder()
        {
            service.CreateUsers([.. AdminUsers, .. NormalUsers]);
        }
    }
}
