using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Banking.Models;
using Banking.Services;
using JWTAuthentication.Authentication;
using Microsoft.AspNet.Identity;

namespace Banking.Controllers
{
    [Authorize(Roles = UserRoles.User)]
    [ApiController]
    [Route("[controller]")]
    public class UserController(ILogger<UserController> logger, IUserService userService, ITransactionService transactionService) : Controller
    {
         
        public IActionResult GetUserData(DateTime start, DateTime end)
        {
            string userId = User.Identity.GetUserId();
            List<UserTransactionModel> transactions = transactionService.GetUserTransaction(userId, start, end);
            return View(transactions);
        }


        [HttpGet]
        public UserModel GetDetail()
        {
            string userId = User.Identity.GetUserId();
            UserModel user = userService.GetUser(userId);
            return user;
        }

        [HttpPut]
        public IActionResult UpdateUserData(UserModel model)
        {
            if(model.Id != User.Identity.GetUserId()){
                //throw
            }
            logger.LogInformation("The user is updating his data");
            userService.UpdateUser(model);
            return RedirectToAction("Index", "GetUserData");
        }
    }
}
