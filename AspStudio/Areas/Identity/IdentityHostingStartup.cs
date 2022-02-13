using System;
using EagleApp.Areas.Identity.Data;
using EagleApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(EagleApp.Areas.Identity.IdentityHostingStartup))]
namespace EagleApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
								services.AddDbContext<AspStudioIdentityDbContext>(options =>
																options.UseSqlServer(
																		context.Configuration.GetConnectionString("DefaultConnection")));
                    services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                            .AddRoles<IdentityRole>()
                            .AddEntityFrameworkStores<AspStudioIdentityDbContext>();
            });
        }
    }
}