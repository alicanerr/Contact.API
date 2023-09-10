using RabbitMQ.Client;
using Report.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddSingleton(sp => new ConnectionFactory() { DispatchConsumersAsync = true });
builder.Services.AddSingleton(sp => new ConnectionFactory());
builder.Services.AddSingleton<RabbitMqClientService>();
builder.Services.AddSingleton<RabbitMqPublisher>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
