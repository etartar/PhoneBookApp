using PhoneBookApp.Core.Infrastructure;
using PhoneBookApp.Core.Infrastructure.EventBus;
using PhoneBookApp.Core.Presentation.ExceptionHandlers;
using PhoneBookApp.Services.Contact.Api.Extensions;
using PhoneBookApp.Services.Contact.Application;
using PhoneBookApp.Services.Contact.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails();

string databaseConnectionString = builder.Configuration.GetConnectionString("Database")!;
var rabbitMqSettings = new RabbitMqSettings(builder.Configuration.GetConnectionString("Queue")!);

builder.Services.AddCoreInfrastructure(
    "PhoneBookApp.Services.Contact.Api",
    [
        ContactInfrastructureConfiguration.ConfigureConsumers
    ],
    rabbitMqSettings);

builder.Services.AddApplication();

builder.Services.AddContactInfrastructure(builder.Configuration, databaseConnectionString);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    await app.ApplyMigrationsAsync();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
