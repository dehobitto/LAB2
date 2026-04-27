using AirlineApp.Data;
using AirlineApp.Repositories;
using AirlineApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<Database>();
builder.Services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
builder.Services.AddScoped<IAirlineRepository, AirlineRepository>();
builder.Services.AddScoped<IAirplaneRepository, AirplaneRepository>();
builder.Services.AddScoped<IAirplaneService, AirplaneService>();

var app = builder.Build();

// always show friendly error page — no stack traces for users
app.UseExceptionHandler("/error");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<Database>();
    DbInitializer.Initialize(db);
}

app.UseStaticFiles();
app.UseRouting();
app.MapGet("/", () => Results.Redirect("/airplanes"));
app.MapControllers();

app.Run();
