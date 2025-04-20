using Mapster;
using PhoneBookApp.Core.Application.Messaging;
using PhoneBookApp.Core.Domain;
using PhoneBookApp.Services.Contact.Domain.Persons;

namespace PhoneBookApp.Services.Contact.Application.Persons.GetPersons;

internal sealed class GetPersonsQueryHandler(IPersonRepository personRepository) : IQueryHandler<GetPersonsQuery, List<GetPersonsDto>>
{
    public async Task<Result<List<GetPersonsDto>>> Handle(GetPersonsQuery request, CancellationToken cancellationToken)
    {
        List<Person> persons = await personRepository.GetListAsync();

        return persons.Adapt<List<GetPersonsDto>>();
    }
}