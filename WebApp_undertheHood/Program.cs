using Microsoft.AspNetCore.Authorization;
using WebApp_undertheHood.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddAuthentication()
    .AddCookie("MyCookieAuth", options =>
    {
        options.Cookie.Name = "MyCookieAuth";
        options.ExpireTimeSpan = TimeSpan.FromSeconds(60);

    });
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("AdminOnly", policy => policy.RequireClaim("Admin"));
        options.AddPolicy("HROnly", policy => policy.RequireClaim("Department", "HR"));
        options.AddPolicy("HRManagerOnly", policy => policy.RequireClaim("Department", "HR").RequireClaim("manager").Requirements.Add(new HRManagerRequirement(6)));
    });

builder.Services.AddSingleton<IAuthorizationHandler, HRManagerRequirementHandler>();


// configure Http client 
builder.Services.AddHttpClient("WebApi", client =>
{
   client.BaseAddress = new Uri("http://localhost:5043"); // match WebApi's launchSettings.json

});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
