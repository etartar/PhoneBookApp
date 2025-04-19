using PhoneBookApp.Core.Application.Abstractions;
using PhoneBookApp.Core.Application.Messaging;
using PhoneBookApp.Core.Domain;
using PhoneBookApp.Services.Contact.Domain.Persons;

namespace PhoneBookApp.Services.Contact.Application.ContactInformations.DeleteContactInformation;

internal sealed class DeleteContactInformationCommandHandler(IPersonRepository personRepository, IUnitOfWork unitOfWork) : ICommandHandler<DeleteContactInformationCommand>
{
    public async Task<Result> Handle(DeleteContactInformationCommand request, CancellationToken cancellationToken)
    {
        ContactInformation? contactInformation = await personRepository.GetContactInformationAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
        
        if (contactInformation is null)
        {
            return Result.Failure(ContactInformationErrors.NotFound(request.Id));
        }

        personRepository.DeleteContactInformation(contactInformation);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
