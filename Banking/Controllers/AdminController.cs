using Banking.Models;
using Banking.Services;
using Banking.ViewModels;
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
            var firstNameClaim = User?.Identity?.Name;

            if (firstNameClaim != null)
            {
                ViewBag.Name = firstNameClaim;
            }

            AdminPageVM model = new AdminPageVM();
            model.Users = userService.GetUsers();
            model.Banks= bankService.GetBanks();
            return View(model);
        }
        public IActionResult UserTransaction(string userId)
        {
            UserTransactionPageVM model = new UserTransactionPageVM();
            var firstNameClaim = User?.Identity?.Name;

            if (firstNameClaim != null)
            {
                ViewBag.Name = firstNameClaim;
            }

            DateTime end = DateTime.Now;
            DateTime start = end.AddMonths(-1);

            model.TransactionModels = transactionService.GetUserTransaction(userId, start, end);

            return View();
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