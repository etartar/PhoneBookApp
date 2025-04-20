using PhoneBookApp.Core.Domain;

namespace PhoneBookApp.Services.Report.UnitTests.Abstractions;

public abstract class BaseTests
{
    public static T AssertDomainEventWasPublished<T>(IEntity entity)
        where T : IDomainEvent
    {
        var domainEvent = entity.DomainEvents.OfType<T>().SingleOrDefault();

        if (domainEvent == null)
        {
            throw new Exception($"{typeof(T).Name} was not published");
        }

        return domainEvent;
    }
}
