using Core.Options;
using Infrastructure.Logger;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Web.API.DI;
using Web.API.Middlewares;

StaticLogger.EnsureInitialized();
Log.BindProperty("", "LifeCycle", true, out _);
Log.Information("Booting Up...");

try
{
var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

// Add services to the container.
services.AddLogging(logBuilder =>
{
    logBuilder.ClearProviders(); // removes all providers from LoggerFactory
    logBuilder.AddConsole();
    logBuilder.AddTraceSource("Information, ActivityTracing"); // Add Trace listener provider
});
services.AddSwagger();
services.AddFeatures();
services.AddIdentity(configuration.GetRequiredSection(AuthenticationOption.Section));
services.AddPersistence(configuration.GetRequiredSection(PersistenceOption.Section));
services.AddFileStorage(configuration.GetRequiredSection(UploadOption.Section));
services.AddMail(configuration.GetRequiredSection(MailOption.Section));
services.AddWorker();
services.Configure<ApiBehaviorOptions>(o =>
{
    o.SuppressModelStateInvalidFilter = true;
});
services.AddControllers();

// Configure the HTTP request pipeline.
var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errorStatusCodes/{0}");
app.UseCors(o => o.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UsePersistence();
app.UseSwagger(true);
// app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
}
catch (Exception ex)
{
    StaticLogger.EnsureInitialized();
    Log.Fatal(ex, "Startup Failed...");
    throw;
}
finally
{
    StaticLogger.EnsureInitialized();
    Log.Information("Shutting Down...");
    Log.CloseAndFlush();
}