using Microsoft.EntityFrameworkCore;
using PhoneBookApp.Core.Infrastructure.Repositories;
using PhoneBookApp.Services.Contact.Domain.Persons;
using PhoneBookApp.Services.Contact.Infrastructure.Database;
using System.Linq.Expressions;

namespace PhoneBookApp.Services.Contact.Infrastructure.Repositories;

internal sealed class PersonRepository : RepositoryBase<Person>, IPersonRepository
{
    public PersonRepository(ContactDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<ContactInformation?> GetContactInformationAsync(Expression<Func<ContactInformation, bool>> predicate, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        IQueryable<ContactInformation> queryable = DbContext.Set<ContactInformation>().AsQueryable();

        if (!enableTracking)
        {
            queryable = queryable.AsNoTracking();
        }

        return await queryable.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task CreateContactInformation(ContactInformation contactInformation, CancellationToken cancellationToken = default)
    {
        await DbContext.Set<ContactInformation>().AddAsync(contactInformation, cancellationToken);
    }

    public void DeleteContactInformation(ContactInformation entity)
    {
        DbContext.Set<ContactInformation>().Remove(entity);
    }
}
