using PhoneBookApp.Core.Application.Messaging;
using PhoneBookApp.Services.Contact.Domain.Persons;

namespace PhoneBookApp.Services.Contact.Application.ContactInformations.CreateContactInformation;

public sealed class CreateContactInformationCommand : ICommand<Guid>
{
    public CreateContactInformationCommand(Guid personId, InformationTypes informationType, string informationContent)
    {
        PersonId = personId;
        InformationType = informationType;
        InformationContent = informationContent;
    }

    public Guid PersonId { get; set; }
    public InformationTypes InformationType { get; set; }
    public string InformationContent { get; set; }
}
