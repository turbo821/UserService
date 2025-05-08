using Microsoft.EntityFrameworkCore;
using UserApi.Data;
using UserApi.Extensions;
using UserApi.Interfaces;
using UserApi.Models;
using UserApi.Repositories;
using UserApi.Services;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("usersdb")); // Or database in memory

string connection = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection)); // Or database in postgresql 


builder.Services.Configure<AdminCredentials>(builder.Configuration.GetSection(nameof(AdminCredentials)));
builder.Services.AddScoped<AdminInitializer>();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
builder.Services.AddScoped<IJwtProvider, JwtProvider>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddAuthorization();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

await app.CreateAdmin();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
