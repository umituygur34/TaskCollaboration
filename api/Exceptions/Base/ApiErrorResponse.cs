

namespace TaskCollaboration.Api.Exceptions.Base;

public class ApiErrorResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string StackTrace { get; set; }
}
