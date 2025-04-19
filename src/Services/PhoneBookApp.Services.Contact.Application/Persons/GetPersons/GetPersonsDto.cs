namespace PhoneBookApp.Services.Contact.Application.Persons.GetPersons;

public class GetPersonsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string CompanyName { get; set; }
}