using BlazorWebApp;
using BlazorWebApp.Components;
using BlazorWebApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BlazorWebAppContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("BlazorWebAppContext") ?? throw new InvalidOperationException("Connection string 'BlazorWebAppContext' not found.")));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddScoped<AuthService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

//public void ConfigureServices(IServiceCollection services)
//{
//    services.AddDbContext<BookContext>(options =>
//        options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

//    // ������ �������
//}
//builder.Services.AddDbContext<BookContext>(options =>
//    options.UseNpgsql(builder.Configuration.GetConnectionString("BookContext") ?? throw new InvalidOperationException("Connection string 'BookContext' not found.")));


app.Run();
