using Microsoft.EntityFrameworkCore;
using MilitaryWeb.BussinessLogic.DataContext;
using MilitaryWeb.BussinessLogic.Service;
using MilitaryWeb.BussinessLogic.Service.Interface;
using MilitaryWeb.Repository;
using MilitaryWeb.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddDbContext<DataContext>(options =>
             options.UseSqlServer("DefaultConnection"));

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
