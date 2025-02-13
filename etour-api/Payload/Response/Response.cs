
using System.Text.Json.Serialization;

namespace etour_api.Payload.Response;

public class Response
{
    [JsonPropertyName("time")]
    public string Time { get; set; }

    [JsonPropertyName("code")]
    public int Code { get; set; }

    [JsonPropertyName("path")]
    public string Path { get; set; }

    [JsonPropertyName("status")]
    public int Status { get; set; } // HttpStatusCode is int in C#

    [JsonPropertyName("message")]
    public string Message { get; set; }

    [JsonPropertyName("exception")]
    public string? Exception { get; set; }

    [JsonPropertyName("data")]
    public Dictionary<string, object> Data { get; set; } = new();

    public Response(string path, int code, string message, string? exception = null, Dictionary<string, object>? data = null)
    {
        Time = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
        Path = path;
        Code = code;
        Status = code; // HttpStatus is an int in C#
        Message = message;
        Exception = exception;
        Data = data ?? new Dictionary<string, object>();
    }
}

 
  
