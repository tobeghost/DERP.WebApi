using DERP.Core.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DERP.WebApi.Infrastructure.StartupConfigure;

public class CommonStartup : IBaseStartup
{
    public int Order => 100;
    
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        //IIS Server Option yüklenmesi sağlandı.
        services.Configure<IISServerOptions>(options =>
        {
            options.AutomaticAuthentication = false;
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment webHostEnvironment)
    {
        if (webHostEnvironment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        
        app.UseHsts();
        
        app.UseHttpsRedirection();
        
        app.UseRouting(); //Must be last
    }
}