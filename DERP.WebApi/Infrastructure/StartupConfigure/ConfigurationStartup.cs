using DERP.WebApi.Infrastructure.Configuration;
using DERP.WebApi.Infrastructure.Extentions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DERP.WebApi.Infrastructure.StartupConfigure;

public class ConfigurationStartup : IBaseStartup
{
    public int Order => 6;

    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions();

        services.AddConfiguration<JwtTokenConfig>(configuration, "JwtToken");
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment webHostEnvironment)
    {
    }
}