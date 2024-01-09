using Banking.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Banking.Controllers
{
    public class SeedController(ISeeder seeder) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            seeder.SeedIfEmpty().Wait();
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "User");

                }
            }
                return RedirectToAction("Login", "Account");
        } 
        public IActionResult Success()
        {
            return View();
        }
    }
}
 