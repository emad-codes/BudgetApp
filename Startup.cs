using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Budget.API.Domain.Repositories;
using Budget.API.Domain.Services;
using Budget.API.Persistence.Contexts;
using Budget.API.Persistence.Repositories;
using Budget.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Budget.API.Extensions;
using Budget.API.Controllers.Config;
using Swashbuckle.AspNetCore.Swagger; using Swashbuckle.AspNetCore.SwaggerUI; using Microsoft.OpenApi.Models; 

namespace Budget.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomSwagger();
            services.AddMemoryCache();
            services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                // Adds a custom error response factory when ModelState is invalid
                options.InvalidModelStateResponseFactory = InvalidModelStateResponseFactory.ProduceErrorResponse;
            });

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MyDbConnection")));
            services.AddControllers();


            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBudgetLevelRepository, BudgetLevelRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBudgetLevelService, BudgetLevelService>();

            services.AddAutoMapper(typeof(Startup));

   
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCustomSwagger();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
