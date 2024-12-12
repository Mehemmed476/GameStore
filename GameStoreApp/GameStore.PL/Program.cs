using GameStore.BL.Services.Abstractions;
using GameStore.BL.Services.Contretes;
using GameStore.DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using GameStore.BL.Services.Concretes;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<GameShopDbContext>(
    opt =>
    {
        opt.UseSqlServer(builder.Configuration.GetConnectionString("MacBookMsSql"));
    }
    );

builder.Services.AddScoped<IGenericCRUDService, GenericCRUDService>();
builder.Services.AddScoped<IFileService, FileService>();

var app = builder.Build();

app.UseStaticFiles();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
