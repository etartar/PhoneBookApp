namespace PhoneBookApp.Services.Contact.Application.Persons.GetPersonById;

public class GetPersonDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string CompanyName { get; set; }

    public List<GetContactInformationDto> ContactInformations { get; set; }
}
