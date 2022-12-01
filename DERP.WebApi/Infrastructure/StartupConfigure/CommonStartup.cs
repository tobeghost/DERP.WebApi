using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DERP.WebApi.Infrastructure.Extentions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DERP.WebApi.Infrastructure.StartupConfigure;

public class CommonStartup : IBaseStartup
{
    public int Order => 1;
    
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSettings();
        
        services.AddLocalization();

        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new List<CultureInfo>()
            {
                new CultureInfo("en"),
                new CultureInfo("tr")
            };

            options.DefaultRequestCulture = new RequestCulture(supportedCultures.FirstOrDefault());
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
            
            options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(context =>
            {
                var languages = context.Request.Headers["Accept-Language"].ToString();
                var currentLanguage = languages.Split(',').FirstOrDefault()?.ToLower();
                var defaultLanguage = string.IsNullOrEmpty(currentLanguage) ? "tr" : currentLanguage;
                if (defaultLanguage != "tr" && defaultLanguage != "en")
                {
                    defaultLanguage = "tr";
                }

                return Task.FromResult(new ProviderCultureResult(defaultLanguage, defaultLanguage));
            }));
        });
        
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

        app.UseRequestLocalization();
        
        app.UseRouting(); //Must be last
    }
}