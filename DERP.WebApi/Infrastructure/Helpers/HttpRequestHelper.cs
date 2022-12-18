using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace DERP.WebApi.Infrastructure.Helpers;

public class HttpRequestHelper
{
    private readonly HttpContext _httpContext;
    
    public HttpRequestHelper(IHttpContextAccessor httpContext)
    {
        _httpContext = httpContext.HttpContext;
    }
    
    public string GetUsername()
    {
        if (_httpContext.Request.Headers.TryGetValue("Authorization", out var authorizationToken))
        {
            var bearerToken = authorizationToken.ToString().Split(' ').LastOrDefault();
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtSecurityTokenHandler.ReadToken(bearerToken);
            if (jwtToken is JwtSecurityToken token)
            {
                var claim = token.Claims.FirstOrDefault(i => i.Type == "Username");
                if (claim != null)
                {
                    return claim.Value;
                }
            }
        }

        return null;
    }
}