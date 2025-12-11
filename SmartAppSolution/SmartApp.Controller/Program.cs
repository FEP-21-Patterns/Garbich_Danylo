using System.Net.Http;
using System.Net.Http.Json; // <-- ПЕРЕКОНАЙТЕСЯ, ЩО ЦЕЙ USING ДОДАНО

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors();
app.UseDefaultFiles(); // Це буде обслуговувати наш новий index.html
app.UseStaticFiles();

var http = new HttpClient();

// ---- МАРШРУТИ ДЛЯ КЕРУВАННЯ (залишаються без змін) ----

app.MapPost("/toggle_light", async () =>
{
    var res = await http.PostAsync("http://localhost:5002/toggle", null);
    return Results.Redirect("http://localhost:5000/");
});
app.MapPost("/toggle_curtains", async () =>
{
    var res = await http.PostAsync("http://localhost:5003/toggle", null);
    return Results.Redirect("http://localhost:5000/");
});
app.MapPost("/toggle_speaker", async () =>
{
    var res = await http.PostAsync("http://localhost:5001/toggle", null);
    return Results.Redirect("http://localhost:5000/");
});
app.MapPost("/toggle_thermostat", async () =>
{
    var res = await http.PostAsync("http://localhost:5004/toggle", null);
    return Results.Redirect("http://localhost:5000/");
});

// ---- [НОВИЙ МАРШРУТ] API для отримання стану всіх пристроїв ----
// ЦЕЙ БЛОК ВИПРАВИТЬ ПОМИЛКУ 404
app.MapGet("/api/devices", async () =>
{
    var devices = new List<object>();
    var client = new HttpClient();

    // Спробуємо отримати стан кожного сервісу
    // Ми використовуємо try-catch, щоб програма не "впала", якщо один із сервісів вимкнено

    try
    {
        // --- Колонка (5001) ---
        var speakerRes = await client.GetAsync("http://localhost:5001/status");
        var speakerStatus = await speakerRes.Content.ReadFromJsonAsync<DeviceStatus>();
        devices.Add(new
        {
            type = "smart_speaker",
            device_id = speakerStatus.Id,
            connection = "connected",
            is_on = speakerStatus.Status == "speaker_on",
            volume = 80 // Примітка: ваші сервіси не повертають гучність, тому це фіктивне значення
        });
    }
    catch (Exception e) { Console.WriteLine($"[Controller] Помилка: сервіс Speaker (5001) недоступний. {e.Message}"); }

    try
    {
        // --- Світло (5002) ---
        var lightRes = await client.GetAsync("http://localhost:5002/status");
        var lightStatus = await lightRes.Content.ReadFromJsonAsync<DeviceStatus>();
        devices.Add(new
        {
            type = "smart_light",
            device_id = lightStatus.Id,
            connection = "connected",
            is_on = lightStatus.Status != "hue_light_off", // "hue_light_status_ok" або інший
            brightness = 60 // Примітка: ваші сервіси не повертають яскравість
        });
    }
    catch (Exception e) { Console.WriteLine($"[Controller] Помилка: сервіс Light (5002) недоступний. {e.Message}"); }

    try
    {
        // --- Штори (5003) ---
        var curtainRes = await client.GetAsync("http://localhost:5003/status");
        var curtainStatus = await curtainRes.Content.ReadFromJsonAsync<DeviceStatus>();
        devices.Add(new
        {
            type = "smart_curtains",
            device_id = curtainStatus.Id,
            connection = "connected",
            is_open = curtainStatus.Status == "curtains_open",
            position = 50 // Примітка: ваші сервіси не повертають позицію
        });
    }
    catch (Exception e) { Console.WriteLine($"[Controller] Помилка: сервіс Curtains (5003) недоступний. {e.Message}"); }

    // Ви можете додати Термостат (5004) сюди за аналогією

    return Results.Ok(devices); // Відправляємо список пристроїв у JSON
});

app.MapGet("/health", () => Results.Ok(new { status = "ok" }));
app.Run("http://localhost:5000");

// [НОВИЙ КЛАС] Допоміжний клас для парсингу JSON-відповідей
public class DeviceStatus
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
}