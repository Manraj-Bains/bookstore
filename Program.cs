using Microsoft.EntityFrameworkCore;
using Bookstore.Data;
using Bookstore.Services;
using bookstore.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<CartService>();

builder.Services.AddScoped<OrderState>(); 

// setup Database Context
var connectionString = builder.Configuration.GetConnectionString("BOOKIESTORE_DB")
   ?? throw new InvalidOperationException("Connection string 'BOOKIESTORE_DB' not found");
builder.Services.AddDbContextFactory<BookstoreDb>(options => options.UseSqlServer(connectionString, sqlServerOptions => sqlServerOptions.EnableRetryOnFailure(
   maxRetryCount: 10,
   maxRetryDelay: TimeSpan.FromSeconds(30),
   errorNumbersToAdd: null)));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    
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
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
