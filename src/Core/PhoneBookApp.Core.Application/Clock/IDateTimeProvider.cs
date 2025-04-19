namespace PhoneBookApp.Core.Application.Clock;

public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }
}
