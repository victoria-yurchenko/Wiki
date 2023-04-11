using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Wiki.Data;
namespace Wiki
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<WikiDb>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("WikiDb") ?? throw new InvalidOperationException("Connection string 'WikiDb' not found.")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Articles/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Articles}/{action=Index}/{id?}");

            app.Run();
        }
    }
}