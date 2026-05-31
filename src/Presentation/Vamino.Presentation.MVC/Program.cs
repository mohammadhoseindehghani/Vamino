using Serilog;
using Vamino.Application;
using Vamino.Infrastructure.EfCore;
using Vamino.Infrastructure.Identity;
using Vamino.Presentation.MVC.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.ConfigureInfrastructureIdentityServices(builder.Configuration);
builder.Services.ConfigureInfrastructureEfCoreServices(builder.Configuration);
builder.Services.ConfigureApplicationServices();

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

app.UseExceptionHandler("/Home/Error");
if (!app.Environment.IsDevelopment())
{


    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();


app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
