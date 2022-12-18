using System;
using DERP.WebApi.Infrastructure.Configuration;
using DERP.WebApi.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace DERP.WebApi.Infrastructure.StartupConfigure;

public class AuthenticationStartup : IBaseStartup
{
    public int Order => 2;
    
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthorization();

        //set default authentication schemes
        //var authenticationBuilder = services.AddAuthentication();
        var authenticationBuilder = services.AddAuthentication(options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        });
        
        authenticationBuilder.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        {
            var config = new JwtTokenConfig();
            configuration.GetSection("JwtToken").Bind(config);

            options.SaveToken = true;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = config.ValidateIssuer,
                ValidateAudience = config.ValidateAudience,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = config.ValidIssuer,
                ValidAudience = config.ValidAudience,
                IssuerSigningKey = JwtSecurityKey.Create(config.SecretKey)
            };
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment webHostEnvironment)
    {
        app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        
        //configure authentication
        app.UseAuthentication();
        app.UseAuthorization();
    }
}