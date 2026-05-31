namespace Vamino.Application.Contracts._Common;

public class AppException(string message, int statusCode = 400, params string[] errors)
    : Exception(message)
{
    public string[] Errors { get; } = errors.Length > 0 ? errors : [message];
    public int StatusCode { get; } = statusCode;
}
