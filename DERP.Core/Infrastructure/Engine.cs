using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DERP.Core.Infrastructure;

public static class Engine
{
    public static void Initialize(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
    }
    
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        //find startup configurations provided by other assemblies
        //create and sort instances of startup configurations
        //configure services
        var startupConfigurations = TypeFinder.FindClassesOfType<IBaseStartup>().OrderBy(i=> i.Order);
        foreach (var startupConfiguration in startupConfigurations)
        {
            startupConfiguration.ConfigureServices(services, configuration);
        }

        //register mapper configurations
        //find mapper configurations provided by other assemblies
        //create and sort instances of mapper configurations
        var mapperConfigurations = TypeFinder.FindClassesOfType<IMapperProfile>().OrderBy(i=> i.Order);
        services.AddAutoMapper((ctx) =>
        {
            foreach (var mapperConfiguration in mapperConfigurations)
            {
                ctx.AddProfile(mapperConfiguration.GetType());
            }
        });
    }
    
    public static void ConfigureRequestPipeline(IApplicationBuilder application, IWebHostEnvironment webHostEnvironment)
    {
        //find startup configurations provided by other assemblies
        //create and sort instances of startup configurations
        //configure request pipeline
        var startupConfigurations = TypeFinder.FindClassesOfType<IBaseStartup>().OrderBy(i=> i.Order);
        foreach (var startupConfiguration in startupConfigurations)
        {
            startupConfiguration.Configure(application, webHostEnvironment);
        }
    }
}