using AutoMapper;
using IntershipTask4.Application;
using IntershipTask4.Application.Requests.Queries.Users;
using IntershipTask4.Domain;
using IntershipTask4.Domain.abstractions;
using IntershipTask4.Domain.Entities;
using IntershipTask4.Infrastructure;
using IntershipTask4.Web.MiddleWare;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace IntershipTask4.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = jwtSettings["Issuer"],
                        ValidAudience = jwtSettings["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
                    };

                    options.Events = new JwtBearerEvents()
                    {
                        //OnChallenge = context =>
                        //{
                        //    context.HandleResponse();
                        //    context.Response.Redirect("/Authentification/Login");
                        //    return Task.CompletedTask;
                        //},

                        OnMessageReceived = context =>
                        {
                            if (context.Request.Cookies.TryGetValue("jwtToken", out var token))
                            {
                                context.Token = token;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
            builder.Services.AddAuthorization();

            builder.Services.AddScoped<IUserRepository, UserRepository>();

            var mappingProfile = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            var mapper = mappingProfile.CreateMapper();
            builder.Services.AddSingleton(mapper);
            builder.Services.AddMediatR(cfg => 
                cfg.RegisterServicesFromAssembly(typeof(GetUsersQuery).Assembly));

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<IntershipTask4Context>(options => options.UseSqlServer(connectionString));

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseMiddleware<ActiveUserMiddleware>();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Authentification}/{action=Login}");

            app.Run();
        }
    }
}
