using PhoneBookApp.Core.Domain;
using System.Linq.Expressions;

namespace PhoneBookApp.Services.Contact.Domain.Persons;

public interface IPersonRepository : IRepository<Person>
{
    Task<ContactInformation?> GetContactInformationAsync(Expression<Func<ContactInformation, bool>> predicate, bool enableTracking = true, CancellationToken cancellationToken = default);
    Task CreateContactInformation(ContactInformation contactInformation, CancellationToken cancellationToken = default);
    void DeleteContactInformation(ContactInformation entity);
}
