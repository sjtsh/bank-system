using Banking.Models;
using Banking.Services;
using JWTAuthentication.Authentication;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Controllers
{
    [Authorize(Roles = UserRoles.User)]
    [ApiController]
    [Route("[controller]")]
    public class TransactionController(ILogger<TransactionController> logger, ITransactionService transactionService) : Controller
    {
        [HttpPost]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTransaction(UserTransactionModel model)
        {
            if (model.SenderId != User.Identity.GetUserId())
            {
                //throw
            }
            logger.LogInformation("A transaction is being created");
            transactionService.CreateTransaction(model); 
            return RedirectToAction("Index", "Transaction");
        }
    }
}
