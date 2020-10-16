using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace Budget.API.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(cfg => 
            {
                cfg.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title ="GBS DnA Budget APIs",
                    Version = "V1",
                    Description="Budget Application key APIs.",
                    Contact = new OpenApiContact
                    {
                        Name = "Emad Afaq Khan ",
                        
                    },
                    License = new OpenApiLicense
                    {
                        Name = "GBS DnA 2020"
                    },
                    
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory,xmlFile);
                cfg.IncludeXmlComments(xmlPath);

            });
            return services;
        }
        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger().UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Budget API");
                options.DocumentTitle = "Budget API";
            });
            return app;
        }
    }
}