using PhoneBookApp.Core.Application.Messaging;

namespace PhoneBookApp.Services.Contact.Application.Persons.GetPersonById;

public sealed class GetPersonByIdQuery : IQuery<GetPersonDto>
{
    public GetPersonByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
