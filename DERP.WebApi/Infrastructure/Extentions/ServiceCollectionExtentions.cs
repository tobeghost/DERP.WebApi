using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            mvcBuilder.AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            //register controllers as services, it'll allow to override them
            mvcBuilder.AddControllersAsServices();

            return mvcBuilder;
        }
}