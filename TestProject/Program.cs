using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TestProject.Data.DbContexts;
using TestProject.Domain.Enums;
using TestProject.Extensions;
using TestProject.Service.Mappers;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().
AddNewtonsoftJson(options =>
options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

builder.Services.AddDbContextPool<TestProjectDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("TestProjectDb")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddCustomerService();
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.AddSwaggerService();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AllPolicy", policy => policy.RequireRole(
        Enum.GetName(UserRole.Admin),
        Enum.GetName(UserRole.Teacher),
        Enum.GetName(UserRole.User)));

    options.AddPolicy("AdminPolicy", policy => policy.RequireRole(
        Enum.GetName(UserRole.Admin)));
    
    options.AddPolicy("StudentPolicy", policy => policy.RequireRole(
        Enum.GetName(UserRole.User)));
    
    options.AddPolicy("TecherPolicy", policy => policy.RequireRole(
        Enum.GetName(UserRole.Teacher)));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.Run();
