using Banking.Models;
using Banking.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Banking.ViewModels;
using JWTAuthentication.Authentication;

namespace Banking.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly IUserService _service;
        private readonly IConfiguration _configuration;
        private readonly IBankService _bankService;
        private readonly ITransactionService _transactionService;
        public AccountController(UserManager<UserModel> userManager, IUserService service, IConfiguration configuration, IBankService bankService, ITransactionService transactionService)
        {
            _userManager = userManager;
            _service = service;
            _configuration = configuration;
            _bankService = bankService;
            _transactionService = transactionService;
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {

                UserModel? user = _service.FindUserByPhone(model.PhoneNumber);
                if (user is null)
                {
                    TempData["invalid-user"] = "Invalid Credential";
                    return View();
                }
                Console.WriteLine(user);
                bool isRightPassword = false;
                if (user != null)
                {
                    isRightPassword = await _userManager.CheckPasswordAsync(user, model.Password);
                }
                if (!isRightPassword)
                {
                    TempData["invalid-user"] = "Invalid Credential";
                    return View();
                }
                if (user != null && isRightPassword)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);

                    var authClaims = new List<Claim>
                {
                    new(ClaimTypes.Name, user.FirstName),
                    new(ClaimTypes.NameIdentifier, user.Id),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }

                    var claimsIdentity = new ClaimsIdentity(authClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    if (user.IsAdmin)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Index", "User");

                    }
                }
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message.ToString();
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login","Account");
        }

        public IActionResult Register()
        {
            RegisterVM model = new RegisterVM();
            model.Banks = _bankService.GetBanks();
            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            try
            {
                UserModel? userExists = _service.FindUserByPhone(model.Phone);
                if (userExists != null)
                {
                    TempData["Invalid-Phone"] = "* Phone number already taken.";
                    return View();
                }
                UserModel user = new UserModel(model.Phone, model.FirstName, model.MiddleName, model.LastName, model.Email, model.Password, model.BankId);
                IdentityResult identity = await _userManager.CreateAsync(user, user.Password);
                if (!identity.Succeeded)
                    return RedirectToAction("Regigster", "Account");
                _transactionService.SignInDeposit(new UserTransactionModel(user.Id, 10000));
                await _userManager.AddToRoleAsync(user, UserRoles.User);
                TempData["register-message"] = "success";
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message.ToString();
                return RedirectToAction("Register", "Account");
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public List<BankModel> GetUserData()
        {
            List<BankModel> banks = _bankService.GetBanks();
            return banks;
        }
    }
}
