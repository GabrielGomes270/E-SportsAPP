using E_SportsAPP.Data;
using E_SportsAPP.Repositories;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Internal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "E-SportsApp",
        Version = "v1",
        Description = "",
    });
});

var configuration = builder.Configuration;
var baseConnectionString = configuration.GetConnectionString("DefaultConnection");

var dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

if (string.IsNullOrEmpty(dbPassword))
{
    throw new InvalidOperationException("A variável de ambiente DB_PASSWORD não foi definida.");
}

var finalConnectionString = baseConnectionString?
    .Replace("Server=localhost", $"Server={dbHost}")
    + $"Password={dbPassword};";


builder.Services.AddMySql<AppDbContext>(
    finalConnectionString,
    ServerVersion.AutoDetect(finalConnectionString),
    mysqlOptions => mysqlOptions.EnableStringComparisonTranslations()
);

builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddCors(options=>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                     .AddEnvironmentVariables();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        c.RoutePrefix = string.Empty;
    });
    
}

app.UseCors("AllowAll");

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
