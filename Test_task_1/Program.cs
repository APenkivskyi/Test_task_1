using Test_task_1.Models;
using Test_task_1.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));
<<<<<<< HEAD
builder.Services.AddSingleton<IMongoDBService ,MongoDBService>();
=======
builder.Services.AddSingleton<MongoDBService>();
>>>>>>> parent of dfc768f (zmiany nazw, stworzenie modelu request)

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
