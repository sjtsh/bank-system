using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Banking.Models;
using Banking.Services;
using JWTAuthentication.Authentication;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Banking.ViewModels;

namespace Banking.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "User")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly IBankService _bankService;
        private readonly ITransactionService _transactionService;
        public UserController(ILogger<UserController> logger, IUserService userService, ITransactionService transactionService, IBankService bankService)
        {
            _logger = logger;
            _userService = userService;
            _transactionService = transactionService;
            _bankService = bankService;
        }
        [HttpPost]
        public IActionResult Index(UserPageVM model)
        {
            var firstNameClaim = User?.Identity?.Name;

            if (firstNameClaim != null)
            {
                ViewBag.Name = firstNameClaim;
            }

            string userId = User?.Identity?.GetUserId();

            if (userId != null)
            {
                model.UserModel = _userService.GetUser(userId);
                model.Banks = _bankService.GetBanks();
            }
            model.TransactionModels = GetUserTransactionData(model);
            return View(model);
        }
        public IActionResult Index()
        {

            var firstNameClaim = User?.Identity?.Name;

            if (firstNameClaim != null)
            {
                ViewBag.Name = firstNameClaim;
            }

            UserPageVM userPageVM = new UserPageVM();

            string userId = User?.Identity?.GetUserId();

            if (userId != null)
            {
                userPageVM.UserModel = _userService.GetUser(userId);
                userPageVM.Banks = _bankService.GetBanks();
            }

            return View(userPageVM);
        }
        public List<UserTransactionModel> GetUserTransactionData(UserPageVM model)
        {
            if(model.StartDate is null)
            {
                model.StartDate = DateTime.Now.Date;
            } 
            if(model.EndDate is null)
            {
                model.EndDate = DateTime.Now.Date;
            } 
            string userId = User.Identity.GetUserId();
            List<UserTransactionModel> transactions = _transactionService.GetUserTransaction(userId, (DateTime) model.StartDate, (DateTime) model.EndDate);
            return transactions;
        }


        [HttpGet]
        public UserModel GetDetail()
        {
            string userId = User.Identity.GetUserId();
            UserModel user = _userService.GetUser(userId);
            return user;
        }

        [HttpPost]
        public IActionResult UpdateUserData(UserPageVM model)
        {
            string userId = User.Identity.GetUserId();

            if (userId != null)
            {
                model.UserModel.Id = userId;
            }

            _logger.LogInformation("The user is updating his data");
            _userService.UpdateUser(model.UserModel);
            return RedirectToAction("Index", "User");
        }
    }
}
