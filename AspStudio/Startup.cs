using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using EagleApp.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using EagleApp.Service;
using EagleApp.Models;
using EagleApp.Helpers;

namespace AspStudio
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

            // configure strongly typed settings object
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            // configure DI for application services
            services.AddScoped<IEmailService, EmailService>();

            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddControllersWithViews().AddRazorRuntimeCompilation(); 
            services.AddRazorPages();
            services.AddScoped<JobLogService>(); 
            services.AddScoped<DashboardService>();
            services.AddScoped<DepartmentService>();
            services.AddScoped<CustomerService>();
            services.AddScoped<StatusService>();
            services.AddScoped<TypeService>();
            services.AddScoped<AuditLogService>();
            services.AddScoped<SavedViewService>();

            services.AddAntiforgery(p => p.SuppressXFrameOptionsHeader = true);
            services.AddAutoMapper(typeof(MappingProfile));
            // services.AddDbContext<EagleDBContext>(item => item.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));  
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");

                await next();
            });


            app.UseEndpoints(endpoints =>
            {
            		endpoints.MapAreaControllerRoute(  
										name: "Identity",  
										areaName: "Identity",  
										pattern: "Identity/{controller=Home}/{action=Index}");  
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");
                endpoints.MapRazorPages();
            });
        }
    }
}
