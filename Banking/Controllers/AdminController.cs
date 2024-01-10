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
            
            model.UserId = userId;

            return View(model);
        }
        [HttpPost]
        public IActionResult UserTransaction(UserTransactionPageVM model)
        {
            try
            {
                var firstNameClaim = User?.Identity?.Name;

                if (firstNameClaim != null)
                {
                    ViewBag.Name = firstNameClaim;
                }

                model.TransactionModels = transactionService.GetUserTransaction(model.UserId, (DateTime)model.StartDate, (DateTime)model.EndDate);
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message.ToString();
            }
            return View(model);
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
            try
            {
                logger.LogInformation("Admin is updating user data");
                userService.UpdateUser(model);
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message.ToString();
            }
            return RedirectToAction("Index", "Admin");
        }

        public IActionResult BankTransaction(int bankId)
        {
            BankTransactionPageVM model = new BankTransactionPageVM();
            var firstNameClaim = User?.Identity?.Name;

            if (firstNameClaim != null)
            {
                ViewBag.Name = firstNameClaim;
            }

            DateTime end = DateTime.Now;
            DateTime start = end.AddMonths(-1);

            model.TransactionModels = transactionService.GetBankTransaction(bankId, start, end);

            model.BankId = bankId;

            return View(model);
        }
        [HttpPost]
        public IActionResult BankTransaction(BankTransactionPageVM model)
        {
            var firstNameClaim = User?.Identity?.Name;

            if (firstNameClaim != null)
            {
                ViewBag.Name = firstNameClaim;
            }

            model.TransactionModels = transactionService.GetBankTransaction((int) model.BankId, (DateTime) model.StartDate, (DateTime)model.EndDate);

            return View(model);
        }
    }
}