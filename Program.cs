using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using NewsPage.Data; // ? DbContext varsa

var builder = WebApplication.CreateBuilder(args);

// E�er DbContext'in varsa:
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NewsDb")));

builder.Services.AddRazorPages();

// ? Do�ru cookie authentication ekleniyor
builder.Services.AddAuthentication("MyCookieAuth")
    .AddCookie("MyCookieAuth", options =>
    {
        options.LoginPath = "/Admin/Login";
        options.AccessDeniedPath = "/AccessDenied";
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication(); // Giri� i�lemleri i�in �art
app.UseAuthorization();

app.MapRazorPages();

app.Run();
