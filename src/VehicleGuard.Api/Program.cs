using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VehicleGuard.Api.Infrastructure.Data;
using VehicleGuard.Shared.Interfaces.EmbeddedDevices;
using VehicleGuard.Shared.Interfaces.Users;
using VehicleGuard.Shared.Interfaces.Gps;
using VehicleGuard.Shared.Interfaces.Vehicle;
using VehicleGuard.Shared.Domain.Models;
using VehicleGuard.Api.Repositories.EmbeddedDevices;
using VehicleGuard.Api.Repositories.Gps;
using VehicleGuard.Api.Repositories.Users;
using VehicleGuard.Api.Repositories.Vehicles;
using VehicleGuard.Api.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Configurando banco de dados
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<VehicleGuardDbContext>(options => options.UseSqlServer(connectionString));

// Desabilitar validação automática
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Configurando openApi
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, ct) =>
    {
        document.Info.Title = "VehicleGuard.API";
        document.Info.Version = "v1.0";
        document.Info.Description = "Surveillance API for vehicle control";
        return Task.CompletedTask;
    });
});

// Configurando JWT
var jwtKeyString = builder.Configuration["JwtSettings:Key"] 
                   ?? throw new InvalidOperationException("JWT Key not found in configuration.");
var key = Encoding.ASCII.GetBytes(jwtKeyString);
builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

// Injeções de dependência
builder.Services.AddTransient<TokenService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IEmbeddedDeviceRepository, EmbeddedDeviceRepository>();
builder.Services.AddScoped<IGpsRepository, GpsRepository>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference(options =>
{
    options.Title = "VehicleGuard API";
    options.Theme = ScalarTheme.DeepSpace;
});

app.Use(async (context, next) =>
{
    // Coloque o seu BREAKPOINT na linha abaixo!
    var authHeader = context.Request.Headers["Authorization"].ToString();
    var todosOsHeaders = context.Request.Headers;

    await next(); // Continua para o próximo middleware (UseAuthentication)
});

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();