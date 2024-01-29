using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Study_Abroad.Data;

[assembly: HostingStartup(typeof(Study_Abroad.Areas.Identity.IdentityHostingStartup))]
namespace Study_Abroad.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
    //public class IdentityHostingStartup : IHostingStartup
    //{
    //    public void Configure(IWebHostBuilder builder)
    //    {
    //        builder.ConfigureServices((context, services) => {
    //            services.AddDbContext<Study_AbroadContext>(options =>
    //                options.UseSqlServer(
    //                    context.Configuration.GetConnectionString("Study_AbroadContextConnection")));

    //            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    //                .AddEntityFrameworkStores<Study_AbroadContext>();
    //        });
    //    }
    //}
}