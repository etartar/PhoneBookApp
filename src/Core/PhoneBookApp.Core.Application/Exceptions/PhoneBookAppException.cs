using PhoneBookApp.Core.Domain;

namespace PhoneBookApp.Core.Application.Exceptions;

public sealed class PhoneBookAppException : Exception
{
    public PhoneBookAppException(string requestName, Error? error = default, Exception? innerException = default)
        : base("Application exception", innerException)
    {
        RequestName = requestName;
        Error = error;
    }

    public string RequestName { get; }

    public Error? Error { get; }
}
