using HW01;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
var app = builder.Build();

app.UseSession();

app.UseFirst();
app.UseSecond();
app.UseThird();

app.Run();