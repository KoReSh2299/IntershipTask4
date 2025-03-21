using IntershipTask4.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace IntershipTask4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<IntershipTask4Context>(options => options.UseSqlServer(connectionString));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();
            //app.UseWelcomePage();

            app.Run(async (context) =>
            {
                context.Response.Redirect("https://dotnet.microsoft.com/ru-ru/apps/ai");
                await context.Response.WriteAsync("");
            });
            app.Run();
        }
    }
}
