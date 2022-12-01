﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DERP.WebApi.Infrastructure.StartupConfigure;

public class AuthenticationStartup : IBaseStartup
{
    public int Order => 2;
    
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment webHostEnvironment)
    {
        //configure authentication
        app.UseAuthentication();
        app.UseAuthorization();
    }
}