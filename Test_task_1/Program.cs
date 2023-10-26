using TestTask1.Interface;
using TestTask1.Models;
using TestTask1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<ICustomerAndOrderRepository, MongoCustomersAndOrdersRepository>();
builder.Services.AddSingleton<IOrderService, OrderService>();
builder.Services.AddSingleton<ICustomerService, CustomerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
