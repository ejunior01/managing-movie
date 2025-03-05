using Microsoft.EntityFrameworkCore;
using MovieManagement.Web.Persistence;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

builder.Services.AddDbContext<MovieDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseNpgsql(connectionString);
});


var app = builder.Build();


// await using (var serviceScope = app.Services.CreateAsyncScope())
// await using (var dbContext = serviceScope.ServiceProvider.GetRequiredService<MovieDbContext>())
// {
//     await dbContext.Database.EnsureCreatedAsync();
// }


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapGet("/", () => "Hello World!").Produces(200, typeof(string));

app.Run();
