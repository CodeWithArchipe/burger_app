using Microsoft.EntityFrameworkCore;
using CUT_Burger.Data;
using CUT_Burger.Interfaces;
using CUT_Burger.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Database context configuration
builder.Services.AddDbContext<SqLiteDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependency injection for database initialization
builder.Services.AddScoped<IDbInitializer, DbInitializerRepo>();

// Session and caching setup
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Authentication configuration
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Path for the login page
        options.LogoutPath = "/Account/Logout"; // Path for logout
        options.AccessDeniedPath = "/Account/AccessDenied"; // Optional: Path for access denied
        options.SlidingExpiration = true; // Optional: enable sliding expiration
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Set the cookie expiration time
    });

// Configure application cookie options
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true; // Make cookie HttpOnly for security
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Use secure cookies (if using HTTPS)
});

var app = builder.Build();

// Error handling and security middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Use session middleware
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Add Authentication Middleware
app.UseAuthentication();
app.UseAuthorization();

// Initialize database during application startup
using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    var context = scope.ServiceProvider.GetRequiredService<SqLiteDbContext>();
    dbInitializer.Initialize(context);
}

// Define default route for the application
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
