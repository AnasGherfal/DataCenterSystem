using ManagementAPI.DI;
using Shared.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var useInMemoryDb = true;
// Add services to the container.
builder.Services.AddCustomControllers();
builder.Services.AddSwagger();
builder.Services.AddCustomCors();
builder.Services.AddServices();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddPersistence(useInMemoryDb, builder.Configuration.GetConnectionString("Persistence")!);


var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
app.UseSwagger(app.Environment.IsDevelopment());
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.MapControllers();
app.UsePersistence(useInMemoryDb);
app.Run();