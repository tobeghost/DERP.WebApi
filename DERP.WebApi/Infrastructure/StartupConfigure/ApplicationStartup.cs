using DERP.Services.Abstract;
using DERP.Services.Concrete;
using DERP.WebApi.Infrastructure.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace DERP.WebApi.Infrastructure.StartupConfigure;

public class ApplicationStartup : IBaseStartup
{
    public int Order => 5;
    
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(typeof(DerpContext), cfg =>
        {
            var connectionString = configuration.GetSection("MongoConnection:ConnectionString").Value;
            var mongoContext = new DerpContext(connectionString);
            return mongoContext;
        });

        services.AddSingleton<IAccountService, AccountService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment webHostEnvironment)
    {
    }
}