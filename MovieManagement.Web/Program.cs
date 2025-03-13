using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieManagement.Web.Behaviors;
using MovieManagement.Web.Endpoints;
using MovieManagement.Web.Exceptions;
using MovieManagement.Web.Persistence;
using Scalar.AspNetCore;
using Serilog;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

// Comentando visto que está sendo utilizado o banco de dados In Memory para desenvolvimento

//builder.Services.AddDbContext<MovieDbContext>(options =>
//{
//    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//    options.UseNpgsql(connectionString);
//});

builder.Services.AddDbContext<MovieDbContext>(options =>
{
    options.UseInMemoryDatabase("MoveDatabase");
});

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    options.AddOpenBehavior(typeof(RequestResponseLoggingBehavior<,>));
    options.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Host.UseSerilog((context, loggerConfiguration) =>
{
    loggerConfiguration.WriteTo.Console();
    loggerConfiguration.ReadFrom.Configuration(context.Configuration);
});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();

    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
        dbContext.Database.EnsureCreated();
    }
}

app.MapMovieEndpoints();
app.UseSerilogRequestLogging();
app.UseExceptionHandler();
app.Run();
