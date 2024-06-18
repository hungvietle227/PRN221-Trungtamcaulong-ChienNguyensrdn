using BadmintonCenter.BusinessObject.Models;
using BadmintonCenter.DataAcess.DAO;
using BadmintonCenter.DataAcess.Repository;
using BadmintonCenter.DataAcess.Repository.Interface;
using BadmintonCenter.Presentation.Middleware;
using BadmintonCenter.Service;
using BadmintonCenter.Service.Interface;
using DemoSchedule.Services;
using DemoSchedule.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<BadmintonDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(optionsCookies =>
{
    optionsCookies.Cookie.Name = "AuthUser";
});

// DAO
builder.Services.AddScoped<IUserDAO, UserDAO>();
builder.Services.AddScoped<ITimeSlotDAO, TimeSlotDAO>();
builder.Services.AddScoped<ICourtDAO, CourtDAO>();
builder.Services.AddScoped<IBookingDetailDAO,  BookingDetailDAO>();
builder.Services.AddScoped<IBookingDAO, BookingDAO>();

// repos
builder.Services.AddScoped<IUserRepository, UserRepo>();
builder.Services.AddScoped<ICourtRepository, CourtRepo>();
builder.Services.AddScoped<ITimeSlotRepository, TimeSlotRepo>();
builder.Services.AddScoped<IBookingDetailRepository, BookingDetailRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepo>();

// services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICourtService, CourtService>();
builder.Services.AddScoped<ITimeSlotService, TimeSlotService>();
builder.Services.AddScoped<ICommonService, CommonService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
