using PlatformService.Data;
using PlatformService.DI;
using PlatformService.SyncDataServices.Grpc;

var builder = WebApplication.CreateBuilder(args);

var env = builder.Environment;
var configuration = builder.Configuration;

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDependencyGroup(env, configuration);
builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.MapGet("/", () => "Platforms Service");
app.MapGrpcService<GrpcPlatformService>();
app.MapGet(
    "/protos/platforms.proto",
    async context =>
    {
        await context.Response.WriteAsync(File.ReadAllText("Protos/platforms.proto"));
    }
);

// Configure the HTTP request pipeline.

app.MapControllers();

//app.UseHttpsRedirection();
PrepDb.PrepPopulation(app, env.IsProduction());

app.Run();
