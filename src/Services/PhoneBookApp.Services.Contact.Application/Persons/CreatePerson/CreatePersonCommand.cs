using PhoneBookApp.Core.Application.Messaging;

namespace PhoneBookApp.Services.Contact.Application.Persons.CreatePerson;

public sealed class CreatePersonCommand : ICommand<Guid>
{
    public CreatePersonCommand(string name, string surname, string companyName)
    {
        Name = name;
        Surname = surname;
        CompanyName = companyName;
    }

    public string Name { get; set; }
    public string Surname { get; set; }
    public string CompanyName { get; set; }
}
