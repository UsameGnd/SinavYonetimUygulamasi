using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SinavYonetimUygulamasi.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using SinavYonetimUygulamasi.Models;
using SinavYonetimUygulamasi.Services;
using Microsoft.AspNetCore.Diagnostics;
using SinavYonetimUygulamasi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register PDF Service
builder.Services.AddScoped<IPdfService, PdfService>();

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add authentication services
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
    });

// Add session services
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Seed the database with test data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        SeedDatabase(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

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

app.UseAuthentication();
app.UseAuthorization();

// Add session middleware
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

// Database seeding method
void SeedDatabase(AppDbContext context)
{
    // Only seed if there are no users
    if (!context.Users.Any())
    {
        // Create a test teacher user
        var testUser = new User
        {
            Username = "teacher",
            Email = "teacher@example.com",
            PasswordHash = "password123", // In a real app, hash this password
            Role = "Egitmen",
            CreatedAt = DateTime.Now
        };

        context.Users.Add(testUser);
        context.SaveChanges();
    }
}
