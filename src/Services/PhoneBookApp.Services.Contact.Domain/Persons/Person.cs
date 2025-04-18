using PhoneBookApp.Core.Domain;

namespace PhoneBookApp.Services.Contact.Domain.Persons;

public class Person : Entity, ISoftDeletable
{
    public Person()
    {
    }

    public Person(Guid id, string name, string surname, string companyName)
    {
        Id = id;
        Name = name;
        Surname = surname;
        CompanyName = companyName;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string CompanyName { get; set; }
    public DateTime? DeletedOn { get; set; }

    public virtual ICollection<ContactInformation> ContactInformations { get; set; } = new HashSet<ContactInformation>();

    public Person CreatePerson(string name, string surname, string companyName)
    {
        Person person = new Person(Guid.NewGuid(), name, surname, companyName);

        return person;
    }
}
