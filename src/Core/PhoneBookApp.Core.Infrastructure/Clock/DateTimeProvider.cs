using PhoneBookApp.Core.Application.Clock;

namespace PhoneBookApp.Core.Infrastructure.Clock;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
