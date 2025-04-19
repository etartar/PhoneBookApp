using PhoneBookApp.Services.Contact.Domain.Persons;

namespace PhoneBookApp.Services.Contact.Application.Persons.GetPersonById;

public class GetContactInformationDto
{
    public Guid Id { get; set; }
    public InformationTypes InformationType { get; set; }
    public string InformationContent { get; set; }
}