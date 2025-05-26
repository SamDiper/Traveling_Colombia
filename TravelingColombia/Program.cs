using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Repository.Implementacion;
using Repository.Interface;
using TravelingColombia.Helpers;
using TravelingColombia.Models;
using TravelingColombia.Repository.Implementacion;
using TravelingColombia.Repository.Interface;
using TravelingColombia.UnitOfWork.Implementacion;
using TravelingColombia.UnitOfWork.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

//cloudinary
builder.Services.AddSingleton(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var settings = config.GetSection("CloudinarySettings").Get<CloudinarySettings>();

    if (string.IsNullOrEmpty(settings?.CloudName))
    {
        throw new ArgumentException("Cloud name is missing in configuration.");
    }

    var account = new Account(settings.CloudName, settings.ApiKey, settings.ApiSecret);
    return new CloudinaryDotNet.Cloudinary(account);
});



// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Cliente/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
        options.AccessDeniedPath = "/Cliente/Login";
    });

// Add ConnectionString
builder.Services.AddDbContext<TravelingColombiabdContext>(opc =>
    opc.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionDefault"))
);

//Dependencias
builder.Services.AddScoped<IUnitUser, UnitUser>();
builder.Services.AddScoped<IRepositoryViaje,RepositoryViaje>();
builder.Services.AddScoped<IRepositoryReserva,RepositoryReserva>();
builder.Services.AddScoped<IRepositoryPlan,RepositoryPlan>();
builder.Services.AddScoped<IRepositoryPago, RepositoryPago>();
builder.Services.AddScoped<IRepositoryUsuario, RepositoryUsuario>();

builder.Services.AddScoped(typeof(IRepositoryGeneric<,>), typeof(RepositoryGeneric<,>));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
