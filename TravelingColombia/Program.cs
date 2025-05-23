using Microsoft.EntityFrameworkCore;
using Repository.Implementacion;
using Repository.Interface;
using TravelingColombia.Models;
using TravelingColombia.Repository.Implementacion;
using TravelingColombia.Repository.Interface;
using TravelingColombia.UnitOfWork.Implementacion;
using TravelingColombia.UnitOfWork.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

// Add ConnectionString
builder.Services.AddDbContext<TravelingColombiabdContext>(opc =>
    opc.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionDefault"))
);

//Dependencias
builder.Services.AddScoped<IUnitUser, UnitUser>();
builder.Services.AddScoped<IRepositoryViaje,RepositoryViaje>();
builder.Services.AddScoped<IRepositoryReserva,RepositoryReserva>();
builder.Services.AddScoped<IRepositoryPlan,RepositoryPlan>();
builder.Services.AddScoped<IRepositoryPago,RepositoryPago>();
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

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
