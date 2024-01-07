using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Banking.Models;
using Banking.Services;

namespace Banking.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController(ILogger<UserController> logger) : ControllerBase
    {
        private readonly IUserService service = new UserService();

        private readonly ILogger<UserController> _logger = logger;

        [HttpGet(Name = "GetUsers")]
        public List<UserModel> Get()
        {
            _logger.LogInformation("GET /User");
            return service.GetUsers();
        }
    }
}
