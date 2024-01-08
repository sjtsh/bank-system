using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Banking.Models;
using Banking.Services;
using JWTAuthentication.Authentication;

namespace Banking.Controllers
{
    [Authorize(Roles = UserRoles.User)]
    [ApiController]
    [Route("[controller]")]
    public class UserController(ILogger<UserController> logger, IUserService userService, ITransactionService transactionService) : Controller
    {

        public IActionResult GetUserData(string userId, DateTime start, DateTime end)
        {
            List<UserTransactionModel> transactions = transactionService.GetUserTransaction(userId, start, end);
            return View(transactions);
        }

        [HttpPut]
        public IActionResult UpdateUserData(UserModel model)
        {
            logger.LogInformation("The user is updating his data");
            userService.UpdateUser(model);
            return RedirectToAction("Index", "GetUserData");
        }
    }
}
