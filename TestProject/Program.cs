using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TestProject.Data.DbContexts;
using TestProject.Extensions;
using TestProject.Service.Mappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
   options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

builder.Services.AddDbContextPool<TestProjectDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("TestProjectDb")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddCustomerService();
builder.Services.AddSwaggerGen(p =>
{
    p.ResolveConflictingActions(ad => ad.FirstOrDefault());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
