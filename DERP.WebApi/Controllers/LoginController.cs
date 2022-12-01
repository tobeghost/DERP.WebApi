using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DERP.Services.Accounts;

namespace DERP.WebApi.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginController : ControllerBase
    {
        private readonly IAccountService _accountService;
        
        public LoginController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn()
        {
            _accountService.Create();
            
            await Task.Delay(1000);
            return Ok();
        }
        
        [HttpPost("sign-out")]
        public async Task<IActionResult> SignOut()
        {
            await Task.Delay(1000);
            return Ok();
        }
    }
}
