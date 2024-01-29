using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Serialization;
using Study_Abroad.Data;
using Study_Abroad.Services.ReadExcelFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study_Abroad
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
            services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddControllers().AddJsonOptions(option =>
            {
                option.JsonSerializerOptions.PropertyNamingPolicy = null;
            }).AddNewtonsoftJson(a => { a.SerializerSettings.ContractResolver = new DefaultContractResolver(); });
            services.AddDbContext<StudyAbroadContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IReadExcelFileInterface, ReadExcelFileService>();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            //services.AddControllersWithViews();

            services.AddIdentity<IdentityUser, IdentityRole>(options => {
                //options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                //options.SignIn.RequireConfirmedEmail = false;
                options.User.RequireUniqueEmail = false;
                //options.User.AllowedUserNameCharacters = false;
            }).AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<StudyAbroadContext>().AddDefaultUI().AddDefaultTokenProviders();
            //services
            //.AddAuthentication(jwoption =>
            //{
            //    //jwoption.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    //jwoption.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    jwoption.DefaultScheme = "JwtBearer_OR_COOKIE";
            //    jwoption.DefaultChallengeScheme = "JwtBearer_OR_COOKIE";
            //})
            //  .AddCookie("AdminCookies", options =>
            //  {
            //      options.LoginPath = "/Identity/Account/Login";
            //      options.AccessDeniedPath = "/Identity/Account/Login";
            //      options.ExpireTimeSpan = TimeSpan.FromDays(2);
            //  })

            //  .AddPolicyScheme("JwtBearer_OR_COOKIE", "JwtBearer_OR_COOKIE", options =>
            //  {
            //      options.ForwardDefaultSelector = context =>
            //      {
            //          string authorization = context.Request.Headers[HeaderNames.Authorization];
            //          var path = context.Request.Path;
            //          if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith("Bearer "))
            //              return JwtBearerDefaults.AuthenticationScheme;
            //          //if (path == "/" || path.ToString().Contains("Home"))
            //          //    return "AdminCookies";
            //          //else
            //          return CookieAuthenticationDefaults.AuthenticationScheme;
            //      };
            //  });

            //services.AddAuthorization(options =>
            //{
            //    var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
            //        JwtBearerDefaults.AuthenticationScheme,
            //        CookieAuthenticationDefaults.AuthenticationScheme,
            //        //"UserCookies",
            //        "AdminCookies");
            //    defaultAuthorizationPolicyBuilder =
            //        defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
            //    options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();

            //    //var vendorCookieSchemePolicyBuilder = new AuthorizationPolicyBuilder(CookieAuthenticationDefaults.AuthenticationScheme);
            //    //options.AddPolicy("VendorCookieScheme", vendorCookieSchemePolicyBuilder
            //    //    .RequireAuthenticatedUser()
            //    //    .Build());
            //    //var userCookieSchemePolicyBuilder = new AuthorizationPolicyBuilder("UserCookies");
            //    //options.AddPolicy("UserCookieScheme", userCookieSchemePolicyBuilder
            //    //    .RequireAuthenticatedUser()
            //    //    .Build());
            //    var adminCookieSchemePolicyBuilder = new AuthorizationPolicyBuilder("AdminCookies");
            //    options.AddPolicy("AdminCookieScheme", adminCookieSchemePolicyBuilder
            //        .RequireAuthenticatedUser()
            //        .Build());

            //});

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(options =>
               {
                   options.LoginPath = "/Identity/Account/Login/";
                   options.LogoutPath = "/Identity/Account/Logout/";
               });

            services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            });


            services.AddRazorPages().AddRazorRuntimeCompilation();






        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
