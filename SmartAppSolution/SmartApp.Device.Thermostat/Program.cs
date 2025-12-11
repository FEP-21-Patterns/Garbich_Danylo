using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<JsonOptions>(o => {
    o.SerializerOptions.PropertyNamingPolicy = null;
});
var app = builder.Build();

string deviceId = "THERMOSTAT-" + Guid.NewGuid().ToString("N").Substring(0, 8);
string deviceName = "Thermostat";
bool connected = false;
double temperature = 21.5;

app.MapGet("/", () => Results.Ok(new { Id = deviceId, Name = deviceName, Host = "localhost", Port = 5004 }));
app.MapPost("/connect", () => {
    connected = true;
    Console.WriteLine($"[{deviceName}] connected");
    return Results.Ok(new { success = true });
});
app.MapPost("/disconnect", () => {
    connected = false;
    Console.WriteLine($"[{deviceName}] disconnected");
    return Results.Ok(new { success = true });
});
app.MapGet("/status", () => {
    var status = connected ? ($"temperature:{temperature:F1}") : "thermostat_off";
    return Results.Ok(new { Id = deviceId, Name = deviceName, Status = status });
});
app.MapPost("/set-temp/{t:double}", (double t) => {
    temperature = t;
    return Results.Ok(new { success = true, temperature });
});

// [ДОДАНО] Новий маршрут /toggle
app.MapPost("/toggle", () => {
    connected = !connected; // Змінюємо стан на протилежний
    Console.WriteLine($"[{deviceName}] Toggled. New state: {(connected ? "ON" : "OFF")}");
    return Results.Ok(new { success = true, state = connected });
});

app.Run("http://localhost:5004");