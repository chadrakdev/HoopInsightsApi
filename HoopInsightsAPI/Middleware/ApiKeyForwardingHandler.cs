namespace HoopInsightsAPI.Middleware;

public class ApiKeyForwardingHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private const string ApiKeyHeaderName = "X-BDL-API-Key";

    public ApiKeyForwardingHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var headers = _httpContextAccessor.HttpContext?.Request.Headers;
        if (headers != null && headers.TryGetValue(ApiKeyHeaderName, out var values))
        {
            var apiKey = values.ToString();
            if (!string.IsNullOrWhiteSpace(apiKey))
            {
                request.Headers.Remove(ApiKeyHeaderName);
                request.Headers.Add("Authorization", apiKey);
            }
        }

        return await base.SendAsync(request, cancellationToken);
    }
}