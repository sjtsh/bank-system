using Banking.Models;
using Banking.Services;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Controllers
{ 
    public class AuthController : Controller
    {
        private readonly IUserService _service;

        public AuthController(IUserService service)
        {
            _service = service;
        }

        public IActionResult Login()
        {
            return View(); 
        }

        public IActionResult Register( )
        {
            return View();
        }
         
        public IActionResult Home()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            UserModel? user = _service.FindUserByPhone(model.PhoneNumber);
            TempData["token"] = "AZxC123S";
            return RedirectToAction("Home", "Auth");
        }

        [HttpPost]
        public IActionResult Register(UserModel model)
        {
            return View();
        }
    }
}
