
namespace etour_api.Exceptions;

public class ApiException : Exception
{
    public ApiException(string message) : base(message)
    {
    }

    public ApiException() : base("An error occured")
    {
    }
}