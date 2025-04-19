using Microsoft.EntityFrameworkCore;
using PhoneBookApp.Core.Application.Abstractions;
using PhoneBookApp.Core.Application.Messaging;
using PhoneBookApp.Core.Domain;
using PhoneBookApp.Services.Contact.Domain.Persons;

namespace PhoneBookApp.Services.Contact.Application.ContactInformations.CreateContactInformation;

internal sealed class CreateContactInformationCommandHandler(IPersonRepository personRepository, IUnitOfWork unitOfWork) : ICommandHandler<CreateContactInformationCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateContactInformationCommand request, CancellationToken cancellationToken)
    {
        Person? person = await personRepository.GetAsync(x => x.Id == request.PersonId,
            include: x => x.Include(p => p.ContactInformations),
            cancellationToken: cancellationToken);

        if (person is null)
        {
            return Result.Failure<Guid>(PersonErrors.NotFound(request.PersonId));
        }

        ContactInformation contactInformation = ContactInformation.Create(request.PersonId, request.InformationType, request.InformationContent);

        await personRepository.CreateContactInformation(contactInformation, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(contactInformation.Id);
    }
}
