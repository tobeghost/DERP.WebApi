using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DERP.Services.Abstract;

namespace DERP.WebApi.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        
        public LoginController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn()
        {
            await _customerService.ValidateCustomer("", "");
            
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
