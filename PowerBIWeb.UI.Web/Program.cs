using PowerBIWeb.Application.Services;
using PowerBIWeb.Models;
using PowerBIWeb.Services;
using PowerBIWeb.UI.Web.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// .NET Native DI Abstraction
builder.Services.AddDependencyInjectionConfiguration();

// Register AadService and PbiEmbedService for dependency injection
builder.Services.AddScoped<AadService>()
                .AddScoped<PbiEmbedService>();

// Loading appsettings.json in C# Model classes
builder.Services.Configure<AzureAd>(builder.Configuration.GetSection("AzureAd"))
                .Configure<PowerBI>(builder.Configuration.GetSection("PowerBI"));

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
