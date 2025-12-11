using SmartApp.Common.Models;
using System.Net.Http;
using System.Net.Http.Json;

public class DeviceFacade
{
    private readonly HttpClient _http;
    private readonly List<DeviceInfo> _devices = new();

    public DeviceFacade(HttpClient http) { _http = http; }

    public void Register(DeviceInfo info) => _devices.Add(info);

    public async Task ConnectAllAsync()
    {
        foreach(var d in _devices)
        {
            var url = $"{d.Host}:{d.Port}/connect";
            try
            {
                var r = await _http.PostAsync(url, null);
                Console.WriteLine($"Connect {d.Name} -> {r.StatusCode}");
            }
            catch(Exception ex) { Console.WriteLine($"Error connect {d.Name}: {ex.Message}"); }
        }
    }

    public async Task DisconnectAllAsync()
    {
        foreach(var d in _devices)
        {
            var url = $"{d.Host}:{d.Port}/disconnect";
            try
            {
                var r = await _http.PostAsync(url, null);
                Console.WriteLine($"Disconnect {d.Name} -> {r.StatusCode}");
            }
            catch(Exception ex) { Console.WriteLine($"Error disconnect {d.Name}: {ex.Message}"); }
        }
    }

    public async Task PrintAllStatusesAsync()
    {
        foreach(var d in _devices)
        {
            var url = $"{d.Host}:{d.Port}/status";
            try
            {
                var resp = await _http.GetFromJsonAsync<StatusResponse>(url);
                if (resp != null)
                    Console.WriteLine($"{d.Name} ({d.Port}) status: {resp.Status}");
            }
            catch(Exception ex) { Console.WriteLine($"Error status {d.Name}: {ex.Message}"); }
        }
    }

    public async Task SetThermostatAsync(double t)
    {
        var thr = _devices.FirstOrDefault(x => x.Name.ToLower().Contains("therm"));
        if (thr == null) { Console.WriteLine("No thermostat registered"); return; }
        var url = $"{thr.Host}:{thr.Port}/set-temp/{t}";
        try
        {
            var r = await _http.PostAsync(url, null);
            Console.WriteLine($"Set thermostat -> {r.StatusCode}");
        }
        catch(Exception ex) { Console.WriteLine($"Error set thermostat: {ex.Message}"); }
    }

    private class StatusResponse { public string Id { get; set; } = ""; public string Name { get; set; } = ""; public string Status { get; set; } = ""; }
}
