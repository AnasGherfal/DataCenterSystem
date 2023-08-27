using Core.Options;
using Microsoft.AspNetCore.Mvc;
using Web.API.DI;
using Web.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

// Add services to the container.
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
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();