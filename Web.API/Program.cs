using Microsoft.AspNetCore.Mvc;
using Shared.Middlewares;
using Web.API.DI;
using Web.API.Options;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

// Add services to the container.
services.AddControllers();
services.AddSwagger();
services.AddFeatures();
services.AddIdentity(configuration.GetRequiredSection(AuthenticationOption.Section));
services.AddPersistence(configuration.GetRequiredSection(PersistenceOption.Section));
services.AddFileStorage(configuration.GetRequiredSection(UploadOption.Section));
services.Configure<ApiBehaviorOptions>(o =>
{
    o.SuppressModelStateInvalidFilter = true;
});

// Configure the HTTP request pipeline.
var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errorStatusCodes/{0}");
app.UseCors(o => o.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UsePersistence();
app.UseSwagger(true);
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();