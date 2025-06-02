namespace Banking.Services
{
    public interface ISeeder
    {
        Task CreateAdmin();
    }
}

namespace Banking.Services
{
    public class SeederSajat(RoleManager<IdentityRole> roleManager, UserManager<UserModel> userManager, IUserService userService, IBankService bankService, ITransactionService transactionService, Context context) : ISeeder
    {
          
        private readonly RoleManager<IdentityRole> RoleManager = roleManager;
        private readonly UserManager<UserModel> UserManager = userManager;
        private readonly Context Context = context;
        private readonly IUserService UserService = userService;
        private readonly IBankService BankService = bankService;
        private readonly ITransactionService TransactionService = transactionService;

        async Task ISeeder.SeedIfEmpty()
        {
            string nameof = "Sajat";
            string email = "sajat@gmail.com";
            string password = "Sajat123";
            string role = "Admin";
            Context.Database.EnsureCreated();
            new BankSeeder(BankService).Seed();
            await new UserSeeder(RoleManager, UserManager, UserService, TransactionService).Seed();
        }
    }    
    public class SeederTej(RoleManager<IdentityRole> roleManager, UserManager<UserModel> userManager, IUserService userService, IBankService bankService, ITransactionService transactionService, Context context) : ISeeder
    {
          
        private readonly RoleManager<IdentityRole> RoleManager = roleManager;
        private readonly UserManager<UserModel> UserManager = userManager;
        private readonly Context Context = context;
        private readonly IUserService UserService = userService;
        private readonly IBankService BankService = bankService;
        private readonly ITransactionService TransactionService = transactionService;

        async Task ISeeder.SeedIfEmpty()
        {
            string nameof = "Sajat";
            string email = "sajat@gmail.com";
            string password = "Sajat123";
            string role = "Admin";
            Context.Database.EnsureCreated();
            new BankSeeder(BankService).Seed();
            await new UserSeeder(RoleManager, UserManager, UserService, TransactionService).Seed();
        }
    }    
    public class SeederAayush(RoleManager<IdentityRole> roleManager, UserManager<UserModel> userManager, IUserService userService, IBankService bankService, ITransactionService transactionService, Context context) : ISeeder
    {
          
        private readonly RoleManager<IdentityRole> RoleManager = roleManager;
        private readonly UserManager<UserModel> UserManager = userManager;
        private readonly Context Context = context;
        private readonly IUserService UserService = userService;
        private readonly IBankService BankService = bankService;
        private readonly ITransactionService TransactionService = transactionService;

        async Task ISeeder.SeedIfEmpty()
        {
            string nameof = "Aayush";
            string email = "aayush@gmail.com";
            string password = "Aayush123";
            string role = "Admin";
            Context.Database.EnsureCreated();
            new BankSeeder(BankService).Seed();
            await new UserSeeder(RoleManager, UserManager, UserService, TransactionService).Seed();
        }
    }
}