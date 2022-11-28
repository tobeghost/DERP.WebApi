using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DERP.WebApi.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginController : ControllerBase
    {
        public LoginController()
        {
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn()
        {
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
