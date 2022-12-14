using DERP.Services.Abstract;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Newtonsoft.Json.Serialization;

namespace DERP.WebApi.Infrastructure.Extentions;

public static class ServiceCollectionExtentions
{
    public static IMvcBuilder AddDerpMvc(this IServiceCollection services)
    {
        //add basic MVC feature
        var mvcBuilder = services.AddMvc();

        mvcBuilder.AddRazorRuntimeCompilation();

        //set compatibility version
        mvcBuilder.SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

        services.AddHsts(options =>
        {
            options.Preload = true;
            options.IncludeSubDomains = true;
        });

        services.AddHttpsRedirection(options =>
        {
            options.RedirectStatusCode = 308;
            options.HttpsPort = 443;
        });

        //MVC now serializes JSON with camel case names by default, use this code to avoid it
        mvcBuilder.AddNewtonsoftJson(options =>
            options.SerializerSettings.ContractResolver = new DefaultContractResolver());

        //register controllers as services, it'll allow to override them
        mvcBuilder.AddControllersAsServices();

        return mvcBuilder;
    }

    public static void AddSettings(this IServiceCollection services)
    {
        var settings = TypeFinder.FindClassesOfType<ISettings>();
        foreach (var setting in settings)
        {
            services.AddScoped(setting.GetType(), (x) =>
            {
                var type = setting.GetType();
                var settingService = x.GetRequiredService<ISettingService>();
                return settingService.LoadSetting(type);
            });
        }
    }

    public static void AddConfiguration<T>(this IServiceCollection services, IConfiguration configuration, string configurationName) where T : class, new()
    {
        services.Configure<T>(configuration.GetSection(configurationName));
        services.AddSingleton(provider => provider.GetService<IOptions<T>>().Value);
    }
}