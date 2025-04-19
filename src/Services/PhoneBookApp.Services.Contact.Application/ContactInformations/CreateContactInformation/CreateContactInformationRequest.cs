using PhoneBookApp.Services.Contact.Domain.Persons;

namespace PhoneBookApp.Services.Contact.Application.ContactInformations.CreateContactInformation;

public class CreateContactInformationRequest
{
    public Guid PersonId { get; set; }
    public InformationTypes InformationType { get; set; }
    public string InformationContent { get; set; }
}