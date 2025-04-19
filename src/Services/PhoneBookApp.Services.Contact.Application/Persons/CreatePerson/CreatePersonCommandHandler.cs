using PhoneBookApp.Core.Application.Abstractions;
using PhoneBookApp.Core.Application.Messaging;
using PhoneBookApp.Core.Domain;
using PhoneBookApp.Services.Contact.Domain.Persons;

namespace PhoneBookApp.Services.Contact.Application.Persons.CreatePerson;

internal sealed class CreatePersonCommandHandler(IPersonRepository personRepository, IUnitOfWork unitOfWork) : ICommandHandler<CreatePersonCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        Person person = Person.Create(request.Name, request.Surname, request.CompanyName);

        await personRepository.AddAsync(person, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(person.Id);
    }
}
