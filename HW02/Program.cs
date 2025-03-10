using HW02;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
var app = builder.Build();

app.UseSession();

app.UseSignUp();
app.UseSignIn();
app.UseOutputInfo();

app.Run();