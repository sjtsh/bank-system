using Banking.Models;
using Banking.Services;
using JWTAuthentication.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Admin")]
    public class AdminController(ILogger<AdminController> logger, IUserService userService, IBankService bankService, ITransactionService transactionService) : Controller
    {
         
        public IActionResult Index()
        {
            List<UserModel> users = userService.GetUsers();
            List<BankModel> banks = bankService.GetBanks();
            return View(users);
        }

        public IActionResult GetUserData(string userId, DateTime start, DateTime end)
        { 
            List<UserTransactionModel> transactions = transactionService.GetUserTransaction(userId, start, end);
            return View(transactions);
        }

        public IActionResult GetBankData(int bankId, DateTime start, DateTime end)
        {
            List<UserTransactionModel> transactions = transactionService.GetBankTransaction(bankId, start, end);
            return View(transactions);
        }

        [HttpPut]
        public IActionResult UpdateUserData(UserModel model)
        {
            logger.LogInformation("Admin is updating user data");
            userService.UpdateUser(model);
            return RedirectToAction("Index", "Admin");
        }
    }
}