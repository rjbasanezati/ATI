using Microsoft.EntityFrameworkCore;
using ATI_IEC.Data;
using Microsoft.AspNetCore.DataProtection;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// Supabase PostgreSQL connection
var connectionString =
    "Host=aws-1-ap-southeast-1.pooler.supabase.com;" +
    "Port=6543;" +
    "Database=postgres;" +
    "Username=postgres.slpneyzlmvdjpjgaeqqa;" +
    "Password=atiwebsite2026;" +
    "SSL Mode=Require;" +
    "Trust Server Certificate=true;" +
    "Pooling=true;";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Session & HTTP
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

// Data Protection (Render-safe)
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo("/app/DataProtection-Keys"))
    .SetApplicationName("ATI_IEC");

var app = builder.Build();

// ❌ NO AUTO MIGRATION HERE

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
app.Run($"http://0.0.0.0:{port}");
