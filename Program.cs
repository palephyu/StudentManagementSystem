using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.DAO;
using StudentManagementSystem.Repositories.Domain;
using StudentManagementSystem.Services;
using StudentManagementSystem.UnitOfWork;
using StudentManagementSystem.Controllers;
using System;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// DbContext
builder.Services.AddDbContext<StudentdbContext>(o => o.UseSqlServer(config.GetConnectionString("DefaultConnection")));

// Add Identity services
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<StudentdbContext>()
    .AddDefaultTokenProviders();

/// Repositories
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IClassRepository, ClassRepository>();
//builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
builder.Services.AddScoped<IExamRepository, ExamRepository>();
builder.Services.AddScoped<IExamResultRepository, ExamResultRepository>();

// UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Services
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IUserService, UserService>();



// Configure Session
builder.Services.AddSession();


builder.Services.AddControllersWithViews();

// register WeatherApiService and HttpClient
builder.Services.AddHttpClient<StudentManagementSystem.Services.WeatherApiService>();


var app = builder.Build();

//HTTPClient for API url
//builder.Services.AddHttpClient<StudentController>();


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

// Middleware
app.UseAuthentication();
app.UseAuthorization();

// Use Session
app.UseSession();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
