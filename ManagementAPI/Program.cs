using Infrastructure;
using ManagementAPI.DI;
using ManagementAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.RegisterServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger(app.Environment.IsDevelopment());
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
/*
using var scope = app.Services.CreateScope();
await using var dbContext = scope.ServiceProvider.GetRequiredService<DataCenterContext>();
await dbContext.Database.MigrateAsync();*/
app.Run();