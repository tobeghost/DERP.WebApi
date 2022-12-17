using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace DERP.WebApi.Infrastructure.Helpers;

public class HttpRequestHelper
{
    private readonly HttpRequest _httpRequest;
    
    public HttpRequestHelper(HttpRequest httpRequest)
    {
        _httpRequest = httpRequest;
    }
    
    public string GetUsername()
    {
        if (_httpRequest.Headers.TryGetValue("Authorization", out var authorizationToken))
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtSecurityTokenHandler.ReadToken(authorizationToken);
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