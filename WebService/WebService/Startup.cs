using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using WebService.DataAccessLayer.Implementations;
using Microsoft.Extensions.Configuration;
using WebService.DataAccessLayer.Interfaces;
using WebService.Services.Interfaces;
using WebService.Services.Implementations;

namespace WebService
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddControllers();
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v_1", new OpenApiInfo { Title = "WebService.API", Version = "v_1" });
            });

            Configuration.Bind("Project", new Config());

            services.AddTransient<IAccessCompany, AccessCompany>();
            services.AddTransient<IAccessDepartment, AccessDepartment>();
            services.AddTransient<IAccessEmployee, AccessEmployee>();
            services.AddTransient<IAccessPassport, AccessPassport>();

            services.AddTransient<DatabaseManager>();

            services.AddTransient<IServiceOfEmployee, ServiceOfEmployee>();



        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v_1/swagger.json", "WebService.API v_1");
            });


            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
