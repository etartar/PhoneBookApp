using PhoneBookApp.Core.Application.Messaging;

namespace PhoneBookApp.Services.Contact.Application.ContactInformations.DeleteContactInformation;

public sealed class DeleteContactInformationCommand : ICommand
{
    public DeleteContactInformationCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
