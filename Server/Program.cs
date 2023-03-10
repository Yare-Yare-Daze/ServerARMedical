using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Server;
using Server.Models;
using Server.Services;

var builder = WebApplication.CreateBuilder(args);

var settings = new Settings();
builder.Configuration.Bind("Settings", settings);
builder.Services.AddSingleton(settings);

// Add services to the container.

builder.Services.AddDbContext<GameDBContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("Db")));

builder.Services.AddControllers().AddNewtonsoftJson(o =>
{
    
});

builder.Services.AddScoped<IProfileService, MockProfileService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
