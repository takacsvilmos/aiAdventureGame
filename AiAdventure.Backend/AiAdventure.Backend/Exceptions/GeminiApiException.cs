using System.Net;

namespace AiAdventure.Backend.Exceptions;

public class GeminiApiException : Exception
{
    public HttpStatusCode StatusCode { get; }
    public string ErrorMessage { get; }

    public GeminiApiException(string message, HttpStatusCode statusCode, string errorMessage) : base(message)
    {
        StatusCode = statusCode;
        ErrorMessage = errorMessage;
    }
}