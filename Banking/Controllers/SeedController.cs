using Banking.Services;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Controllers
{
    public class SeedController(ISeeder seeder) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            seeder.SeedIfEmpty().Wait();
            return RedirectToAction("Success", "Seed");
        } 
        public IActionResult Success()
        {
            return View();
        }
    }
}
 