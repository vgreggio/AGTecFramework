namespace AGTec.Common.Repository.Search.Exceptions;

public class InvalidResponseException: System.Exception
{
    public InvalidResponseException() : base() { }
    public InvalidResponseException(string message) : base(message) { }
    public InvalidResponseException(string message, System.Exception innerException) : base(message, innerException) { }
}