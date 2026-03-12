using Microsoft.EntityFrameworkCore;
using ATI_IEC.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// PostgreSQL (Render)

    var connectionString =
    "Host=dpg-d6p0nhn5gffc738rnckg-a;" +
    "Port=5432;" +
    "Database=atidbxi;" +
    "Username=atidbxi_user;" +
    "Password=G5Hpo4VysyFwOGqdiEn6cQWlf9uXBmxu;" +
    "SSL Mode=Require;" +
    "Trust Server Certificate=true;";


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Sessions
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

//  DO NOT auto-migrate on Render
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
 //   db.Database.Migrate(); // LOCAL ONLY
}

app.UseStaticFiles();
app.UseRouting();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
//app.Run($"http://0.0.0.0:{port}");
app.Run("http://localhost:5000");  // FOR LOCAL TEST

