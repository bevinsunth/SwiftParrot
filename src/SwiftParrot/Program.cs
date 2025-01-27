//why use global?
global using FluentValidation;
global using Refit;
using SwiftParrot;


//TODO: Add logging for startup

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServices();
var app = builder.Build();
app.Configure();
app.Run();

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }