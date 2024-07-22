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

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(optionsCookies =>
{
    optionsCookies.Cookie.Name = "AuthUser";
    optionsCookies.AccessDeniedPath = "/Auth/AccessDenied";
    optionsCookies.LoginPath = "/Auth/Login";
    optionsCookies.LogoutPath = "/Auth/Logout";
});

// DAO
builder.Services.AddScoped<IUserDAO, UserDAO>();
builder.Services.AddScoped<ITimeSlotDAO, TimeSlotDAO>();
builder.Services.AddScoped<ICourtDAO, CourtDAO>();
builder.Services.AddScoped<IBookingDetailDAO, BookingDetailDAO>();
builder.Services.AddScoped<IBookingDAO, BookingDAO>();
builder.Services.AddScoped<IUserPackageDAO, UserPackageDAO>();
builder.Services.AddScoped<ITransactionDAO, TransactionDAO>();
builder.Services.AddScoped<IPaymentMethodDAO, PaymentMethodDAO>();
builder.Services.AddScoped<IPackageDAO, PackageDAO>();
builder.Services.AddScoped<IRoleDAO, RoleDAO>();

// repos
builder.Services.AddScoped<IUserRepository, UserRepo>();
builder.Services.AddScoped<ICourtRepository, CourtRepo>();
builder.Services.AddScoped<ITimeSlotRepository, TimeSlotRepo>();
builder.Services.AddScoped<IBookingDetailRepository, BookingDetailRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepo>();
builder.Services.AddScoped<IUserPackageRepository, UserPackageRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
builder.Services.AddScoped<IPackageRepository, PackageRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

// services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICourtService, CourtService>();
builder.Services.AddScoped<ITimeSlotService, TimeSlotService>();
builder.Services.AddScoped<ICommonService, CommonService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IVnPayService, VnPayService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IUserPackageService, UserPackageService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IPaymentMethodService, PaymentMethodService>();
builder.Services.AddScoped<IPackageService, PackageService>();
builder.Services.AddScoped<IEmailService, EmailService>();

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
