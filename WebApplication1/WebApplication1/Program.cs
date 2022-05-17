using WebApplication1.Controllers;

using EasyNetQ;
using WebApplication1;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSingleton<IWebApplication1Storage, WebApplication1CsvFileStorage>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//var bus = RabbitHutch.CreateBus(builder.Configuration.GetConnectionString("amqp://user:rabbitmq@localhost:5672"));
//builder.Services.AddSingleton<IBus>(bus);

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
