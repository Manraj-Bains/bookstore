using Microsoft.EntityFrameworkCore;
using Bookstore.Data;
using Bookstore.Services;
using bookstore.Components;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<CartService>();

builder.Services.AddScoped<OrderState>(); 

// setup Database Context
var connectionString = builder.Configuration.GetConnectionString("BOOKSTORE_DB");
if (!string.IsNullOrWhiteSpace(connectionString))
{
    builder.Services.AddDbContextFactory<BookstoreDb>(options => options.UseSqlServer(connectionString, sqlServerOptions => sqlServerOptions.EnableRetryOnFailure(
        maxRetryCount: 10,
        maxRetryDelay: TimeSpan.FromSeconds(30),
        errorNumbersToAdd: null)));
}
else
{
    var dataDir = Path.Combine(builder.Environment.ContentRootPath, "App_Data");
    Directory.CreateDirectory(dataDir);
    var sqlitePath = Path.Combine(dataDir, "bookstore.db");
    builder.Services.AddDbContextFactory<BookstoreDb>(options => options.UseSqlite($"Data Source={sqlitePath}"));
}

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<BookstoreDb>();
    dbContext.Database.EnsureCreated();
    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);

// HTTPS redirection for Azure App Service
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
