//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using Microsoft.AspNetCore.Identity;
using RealNest.Web.Brokers.Storages;
using RealNest.Web.Models.Foundations.Users;
using RealNest.Web.Services.Foundations.Houses;
using RealNest.Web.Services.Foundations.Pictures;
using RealNest.Web.Services.Foundations.Users;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();
        builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

        RegisterBrokers(builder);
        RegisterFoundations(builder);

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }

    private static void RegisterBrokers(WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IStorageBroker, StorageBroker>();
    }

    private static void RegisterFoundations(WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IUserService, UserService>();
        builder.Services.AddTransient<IHouseService, HouseService>();
        builder.Services.AddTransient<IPictureService, PictureService>();
    }
}