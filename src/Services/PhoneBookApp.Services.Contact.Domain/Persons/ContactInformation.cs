namespace PhoneBookApp.Services.Contact.Domain.Persons;

public class ContactInformation
{
    public ContactInformation()
    {
    }

    public ContactInformation(Guid id, Guid personId, InformationTypes ınformationType, string ınformationContent)
    {
        Id = id;
        PersonId = personId;
        InformationType = ınformationType;
        InformationContent = ınformationContent;
    }

    public Guid Id { get; set; }
    public Guid PersonId { get; set; }
    public InformationTypes InformationType { get; set; }
    public string InformationContent { get; set; }

    public virtual Person Person { get; set; }
}