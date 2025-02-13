
using etour_api.Payload.Response;

namespace etour_api.Utils;

public static class RequestUtils
{
    public static Response GetResponse(string path, int code, string message, string? exception = null, Dictionary<string, object>? data = null)
    {
        return new Response(path, code, message, exception, data);
    }
}


  