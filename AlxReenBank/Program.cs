using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AlxReenBank.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using ReenBank.Utility;
using ReenBank.Data.Repository.IRepository;
using ReenBank.Data.Repository;
using ReenBank.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();

//var connectionString = builder.Configuration.GetConnectionString("AlxReenBankContextConnection") ?? throw new InvalidOperationException("Connection string 'AlxReenBankContextConnection' not found.");

builder.Services.AddDbContext<AlxReenBankContext>(options => options.UseSqlServer("name=AlxReenBank"));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddDefaultTokenProviders().AddEntityFrameworkStores<AlxReenBankContext>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddSingleton<IEmailSender, EmailSender>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});
// Add services to the container.
builder.Services.AddControllersWithViews();

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
app.MapRazorPages();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Members}/{controller=Home}/{action=Index}/{id?}");

app.Run();
