using System;
using System.Collections.Generic;
using DERP.Core.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DERP.WebApi.Infrastructure.StartupConfigure;

public class SwaggerStartup : IBaseStartup
{
    public int Order => 900;
    
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMvc().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            options.SerializerSettings.DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ss";
            options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver()
            {
                NamingStrategy = null,
            };

            options.SerializerSettings.Converters.Add(new StringEnumConverter());
        });

        services.AddSwaggerGen(c =>
        {
            c.MapType<TimeSpan>(() => new OpenApiSchema { Type = "string", Format = "time-span" });
            //c.MapType<DateTime>(() => new OpenApiSchema() { Type = "string", Format = "date-time" });

            c.SwaggerDoc("v1", new OpenApiInfo { Title = "DERP UAPI", Version = "v1" });
            c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, //Name the security scheme
                new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = JwtBearerDefaults
                                .AuthenticationScheme, //The name of the previously defined security scheme.
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new List<string>()
                }
            });

            c.EnableAnnotations();
        });
    }

    public void Configure(IApplicationBuilder application, IWebHostEnvironment webHostEnvironment)
    {
        application.UseSwagger();
        application.UseSwaggerUI(c =>
        {
            c.RoutePrefix = "";
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "DERP API");
        });
    }
}