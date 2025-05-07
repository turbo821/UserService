
using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.Extensions;
using UserService.Models;
using UserService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("usersdb"));

builder.Services.Configure<AdminCredentials>(builder.Configuration.GetSection(nameof(AdminCredentials)));
builder.Services.AddScoped<AdminInitializer>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
