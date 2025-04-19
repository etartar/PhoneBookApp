using PhoneBookApp.Core.Application.Abstractions;
using PhoneBookApp.Core.Application.Messaging;
using PhoneBookApp.Core.Domain;
using PhoneBookApp.Services.Contact.Domain.Persons;

namespace PhoneBookApp.Services.Contact.Application.Persons.DeletePerson;

internal sealed class DeletePersonCommandHandler(IPersonRepository personRepository, IUnitOfWork unitOfWork) : ICommandHandler<DeletePersonCommand>
{
    public async Task<Result> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        Person? person = await personRepository.GetAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (person is null)
        {
            return Result.Failure(PersonErrors.NotFound(request.Id));
        }

        personRepository.Delete(person);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
