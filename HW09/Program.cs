using HW09.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(15);
    options.Cookie.Name = "UserLogin";
});

//Database
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<GuestBookContext>(options => options.UseSqlServer(connection));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/GuestBook/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=GuestBook}/{action=Index}/{id?}");

app.Run();