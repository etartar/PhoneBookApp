using Mapster;
using Microsoft.EntityFrameworkCore;
using PhoneBookApp.Core.Application.Messaging;
using PhoneBookApp.Core.Domain;
using PhoneBookApp.Services.Contact.Domain.Persons;

namespace PhoneBookApp.Services.Contact.Application.Persons.GetPersonById;

internal sealed class GetPersonByIdQueryHandler(IPersonRepository personRepository) : IQueryHandler<GetPersonByIdQuery, GetPersonDto>
{
    public async Task<Result<GetPersonDto>> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
    {
        Person? person = await personRepository.GetAsync(x => x.Id == request.Id,
            include: p => p.Include(c => c.ContactInformations),
            cancellationToken: cancellationToken);

        if (person is null)
        {
            return Result.Failure<GetPersonDto>(PersonErrors.NotFound(request.Id));
        }

        GetPersonDto personData = person.Adapt<GetPersonDto>();

        return personData;
    }
}