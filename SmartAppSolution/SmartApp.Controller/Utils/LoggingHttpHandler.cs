using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

public class LoggingHttpHandler : DelegatingHandler
{
    public LoggingHttpHandler(HttpMessageHandler inner) : base(inner) {}

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"[HTTP] -> {request.Method} {request.RequestUri}");
        var response = await base.SendAsync(request, cancellationToken);
        Console.WriteLine($"[HTTP] <- {(int)response.StatusCode} {response.ReasonPhrase} ({request.RequestUri})");
        return response;
    }
}
