//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RealNest.Web.Brokers.Storages;
using RealNest.Web.Models.Foundations.Admins;
using RealNest.Web.Models.Foundations.Users;
using RealNest.Web.Services.Foundations.Houses;
using System;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        builder.Services.AddTransient<IStorageBroker, StorageBroker>();
        builder.Services.AddTransient<IHouseService, HouseService>();

        builder.Services.AddSession();
        builder.Services.AddAuthorization();
        builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        builder.Services.AddScoped<IPasswordHasher<Admin>, PasswordHasher<Admin>>();

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
        app.UseSession();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.Run();
    }
}

