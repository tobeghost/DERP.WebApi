using DERP.Core.Infrastructure;
using DERP.WebApi.Infrastructure.Extentions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DERP.WebApi.Infrastructure.StartupConfigure;

public class MvcStartup : IBaseStartup
{
    public int Order => 1000;
    
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDerpMvc();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment webHostEnvironment)
    {
        //MVC endpoint routing
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}