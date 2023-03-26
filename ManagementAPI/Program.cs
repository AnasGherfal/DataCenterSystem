using Infrastructure;
using Infrastructure.Models;
using ManagementAPI.DI;
using ManagementAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCustomControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwagger();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<ServiceServices>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<DataCenterContext>(c => c.UseSqlServer(builder.Configuration.GetConnectionString("LTTDC")));
//builder.Services.AddDbContext<DataCenterContext>(c => c.UseInMemoryDatabase("ltt_inmemory_database"));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger(app.Environment.IsDevelopment());
app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

using var scope = app.Services.CreateScope();
await using var dbContext = scope.ServiceProvider.GetRequiredService<DataCenterContext>();
await dbContext.Database.MigrateAsync();
app.Run();