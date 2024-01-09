using Banking.Models;
using Banking.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using JWTAuthentication.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace Banking.Controllers
{
    public class AuthController(IUserService service, IBankService bankService, IConfiguration configuration, UserManager<UserModel> userManager) : Controller
    {
        private readonly UserManager<UserModel> _userManager = userManager;
        private readonly IUserService _service = service;
        private readonly IConfiguration _configuration = configuration;
        private readonly IBankService _bankService = bankService;

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            UserModel? user = _service.FindUserByPhone(model.PhoneNumber);
            Console.WriteLine(user);
            bool isRightPassword = false;
            if (user != null) {
                isRightPassword = await _userManager.CheckPasswordAsync(user, model.Password);
            }
            if (user != null && isRightPassword)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                Console.WriteLine(userRoles);

                var authClaims = new List<Claim>
                {
                    new(ClaimTypes.Name, user.GetPhoneNumber()),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                Console.WriteLine(authClaims);
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));

                Console.WriteLine(authSigningKey);
                var token = new JwtSecurityToken(
                    expires: DateTime.Now.AddHours(1),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );
                Console.WriteLine(token);
                TempData["token"] = new JwtSecurityTokenHandler().WriteToken(token);
                Console.WriteLine(TempData["token"]);
                if (user.IsAdmin)
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return RedirectToAction("GetUserData", "User");

                }
            }
            return RedirectToAction("Login", "Auth");
        }

        [HttpPost] 
        public async Task<IActionResult> Register(UserModel model)
        {
            UserModel? userExists = _service.FindUserByPhone(model.GetPhoneNumber());
            if (userExists != null)
            {
                return RedirectToAction("Login", "Auth"); 
            }
            IdentityResult identity = await _userManager.CreateAsync(model, model.Password);
            if (!identity.Succeeded)
                return RedirectToAction("SignUp", "Auth"); 
            await _userManager.AddToRoleAsync(model, UserRoles.User);
            return RedirectToAction("Login", "Auth");
        }

        [HttpGet]
        public List<BankModel> GetUserData()
        {
            List<BankModel> banks = bankService.GetBanks();
            return banks;
        }

    }
}
 