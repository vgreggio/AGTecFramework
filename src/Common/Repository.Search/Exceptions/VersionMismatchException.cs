namespace AGTec.Common.Repository.Search.Exceptions;

public class VersionMismatchException : System.Exception
{
    public VersionMismatchException() : base() { }
    public VersionMismatchException(string message) : base(message) { }
    public VersionMismatchException(string message, System.Exception innerException) : base(message, innerException) { }
}