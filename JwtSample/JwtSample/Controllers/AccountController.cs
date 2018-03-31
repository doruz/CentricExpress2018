using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JwtSample.Controllers
{
    [Produces("application/json")]
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly JwtTokenFactory jwtTokenFactory;

        public AccountController(UserManager<User> userManager, JwtTokenFactory jwtTokenFactory)
        {
            this.userManager = userManager;
            this.jwtTokenFactory = jwtTokenFactory;
        }

        [Authorize]
        [HttpGet("hello")]
        public IActionResult SayHello()
        {
            return Ok($"Hello '{User.Identity.Name}'!");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserRegistrationModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await CreateUser(userModel);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("user account was created");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginCredentialsModel credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isValid = await IsValidUser(credentials);
            if (!isValid)
            {
                return BadRequest("provided credentials are not valid");
            }

            var user = await userManager.FindByNameAsync(credentials.UserName);
            var token = jwtTokenFactory.CreateToken(user);

            return Ok(token);
        }

        private async Task<bool> IsValidUser(LoginCredentialsModel credentials)
        {
            var user = await userManager.FindByNameAsync(credentials.UserName);
            if (user != null)
            {
                return await userManager.CheckPasswordAsync(user, credentials.Password);
            }

            return false;
        }

        private async Task<IdentityResult> CreateUser(UserRegistrationModel userModel)
        {
            var user = new User(userModel.UserName);
            return await userManager.CreateAsync(user, userModel.Password);
        }
    }
}