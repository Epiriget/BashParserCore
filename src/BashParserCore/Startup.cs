using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BashParserCore.Data;
using BashParserCore.Models;
using BashParserCore.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace BashParserCore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 8;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<CurrentUserService>();

            //  services.AddTransient<IIdentityService, IdentityService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715
            DatabaseInitialize(app.ApplicationServices);
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }
        public async void DatabaseInitialize(IServiceProvider serviceProvider)
        {
            UserManager<ApplicationUser> userManager =
            serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            RoleManager<IdentityRole> roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (await roleManager.FindByNameAsync("User") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            var moder = new ApplicationUser { UserName = "moderator@gmail.com", Email = "moderator@gmail.com" };
            if (!roleManager.RoleExistsAsync("Moderator").Result)
            {
                await roleManager.CreateAsync(new IdentityRole("Moderator"));
            }
            if (!userManager.Users.Where(p => p.UserName == moder.UserName).Any())
            {
                userManager.CreateAsync(moder, "Moderator1-password").Wait();
                userManager.AddToRolesAsync(moder, new List<string>() { "User", "Moderator" }).Wait();
            }

            var admin = new ApplicationUser { UserName = "admin@gmail.com", Email = "admin@gmail.com" };
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (!userManager.Users.Where(p => p.UserName == admin.UserName).Any())
            {
                userManager.CreateAsync(admin, "Admin1_password").Wait();
                userManager.AddToRolesAsync(admin, new List<string>() { "User", "Moderator", "Admin" }).Wait();
            }

            var mainAdmin = new ApplicationUser { UserName = "mainadmin@gmail.com", Email = "mainadmin@gmail.com" };
            if (!roleManager.RoleExistsAsync("MainAdmin").Result)
            {
                await roleManager.CreateAsync(new IdentityRole("MainAdmin"));
            }
            if (!userManager.Users.Where(p => p.UserName == mainAdmin.UserName).Any())
            {
                userManager.CreateAsync(mainAdmin, "mainadmin_password").Wait();
                userManager.AddToRolesAsync(mainAdmin, new List<string>() { "User", "Moderator", "Admin" }).Wait();
            }

        }
    }
}
