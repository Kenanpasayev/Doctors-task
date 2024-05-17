using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication12.DAL;
using WebApplication12.Models;

namespace WebApplication12
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            }
            );

            // Add services to the container.
           builder.Services.AddControllersWithViews();

            var app = builder.Build();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
          );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}