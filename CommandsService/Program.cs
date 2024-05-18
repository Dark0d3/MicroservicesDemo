using CommandsService.Data;
using CommandsService.DI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDependencyGroup();
builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.MapGet("/", () => "Commands Service!");

// Configure the HTTP request pipeline.

app.MapControllers();

//app.UseHttpsRedirection();
PrepDb.PrepPopulation(app);
app.Run();
