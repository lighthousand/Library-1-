using Library.Data;
using Library.Factories;
using Library.Models;
using Library.Models.Interfaces;
using Library.ViewModels;
using Library.ViewModels.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

ConfigureServices(builder.Services);

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

void ConfigureServices(IServiceCollection services)
{
    // Add the entity framework support
    builder.Services.AddDbContext<LibraryDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("LibraryConnectionString")));

    // Add model dependencies
    builder.Services.AddModelsAbstractFactory<IBook, Book>();
    builder.Services.AddModelsAbstractFactory<IAddBookViewModel, AddBookViewModel>();
    builder.Services.AddModelsAbstractFactory<IUpdateBookViewModel, UpdateBookViewModel>();
}