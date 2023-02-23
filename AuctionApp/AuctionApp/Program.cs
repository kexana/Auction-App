using AuctionApp.Data;
using AuctionApp.Data.Models;
using AuctionApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using AuctionApp.Seed;

namespace AuctionApp.Web
{
    public class Program
    {
        public static void ConfigureServices(WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<AuctionAppDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Forum Services
            builder.Services.AddTransient<IAuctionItemService, AuctionItemService>();

            //builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentity<AuctionUser, IdentityRole>
                    (options => {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 3;
                })
                .AddEntityFrameworkStores<AuctionAppDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddRazorPages();
            builder.Services.AddHttpContextAccessor();


        }

        private static void ConfigureAndRunApplication(WebApplicationBuilder builder)
        {
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.SeedRoles();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });

            app.Run();
        }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder);
            ConfigureAndRunApplication(builder);
        }
    }
}






