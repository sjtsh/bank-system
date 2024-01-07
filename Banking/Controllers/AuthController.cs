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
    public class AuthController(IUserService service, IConfiguration configuration, UserManager<UserModel> userManager) : Controller
    {
        private readonly UserManager<UserModel> _userManager = userManager;
        private readonly IUserService _service = service;
        private readonly IConfiguration _configuration = configuration;

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [Authorize(Roles = UserRoles.User)]
        public IActionResult Home()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            Console.WriteLine("Logining in");
            UserModel? user = _service.FindUserByPhone(model.PhoneNumber);
            Console.WriteLine(user);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
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
                return RedirectToAction("Home", "Auth");
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
            UserModel createdUser = _service.CreateUser(model);
            await _userManager.AddToRoleAsync(createdUser, UserRoles.User);
            return RedirectToAction("Login", "Auth");
        }
    }
}
