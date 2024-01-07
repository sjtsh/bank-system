using Banking.Services;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Controllers
{
    public class SeedController(ISeeder seeder) : Controller
    {
        public IActionResult Index()
        {
            seeder.SeedIfEmpty();
            return View();
        }
    }
}
 