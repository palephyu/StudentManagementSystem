using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.DAO;
using StudentManagementSystem.Repositories.Domain;
using StudentManagementSystem.Services;
using StudentManagementSystem.UnitOfWork;
using System;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration; 
builder.Services.AddDbContext<StudentdbContext>(o => o.UseSqlServer(config.GetConnectionString("DefaultConnection")));
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork >();
builder.Services.AddTransient<IStudentService, StudentService>();
builder.Services.AddScoped<IClassRepository, ClassRepository>();
//builder.Services.AddScoped<IUserRepository, UserRepository>();



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
