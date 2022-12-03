using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DERP.Services.Abstract;
using DERP.WebApi.Domain.Dtos.Customer;
using DERP.WebApi.Infrastructure.Configuration;
using DERP.WebApi.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace DERP.WebApi.Controllers;

[ControllerName("login")]
public class LoginController : BaseController
{
    private readonly ICustomerService _customerService;
    private readonly JwtTokenConfig _jwtTokenConfig;

    public LoginController(ICustomerService customerService,
        JwtTokenConfig jwtTokenConfig)
    {
        _customerService = customerService;
        _jwtTokenConfig = jwtTokenConfig;
    }

    [AllowAnonymous]
    [HttpPost("sign-in")]
    [ProducesResponseType(typeof(SignInResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> SignIn(SignInRequest signInRequest)
    {
        var result = await _customerService.ValidateCustomer(signInRequest.Username, signInRequest.Password);
        if (result == "Successful")
        {
            var claims = new Dictionary<string, string>();
            claims.Add("Username", signInRequest.Username);

            var token = new JwtTokenBuilder();
            token.AddSecurityKey(JwtSecurityKey.Create(_jwtTokenConfig.SecretKey));
            token.AddClaims(claims);
            token.AddExpiry(_jwtTokenConfig.ExpiryInMinutes);

            if (_jwtTokenConfig.ValidateIssuer)
                token.AddIssuer(_jwtTokenConfig.ValidIssuer);

            if (_jwtTokenConfig.ValidateAudience)
                token.AddAudience(_jwtTokenConfig.ValidAudience);

            var jwt = token.Build();

            var re = new SignInResponse();
            re.Username = signInRequest.Username;
            re.Token = jwt.Value;
            re.ExpiryInMinutes = _jwtTokenConfig.ExpiryInMinutes;

            return Ok(re);
        }

        if (result == "CustomerNotExist")
        {
            return NotFound();
        }

        return BadRequest();
    }

    [HttpPost("sign-out")]
    public async Task<IActionResult> SignOut()
    {
        await Task.Delay(1000);
        return Ok();
    }
}