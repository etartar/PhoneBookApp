namespace PhoneBookApp.Services.Contact.Domain.Persons;

public class ContactInformation
{
    public ContactInformation()
    {
    }

    public ContactInformation(Guid id, Guid personId, InformationTypes ınformationType, string informationContent)
    {
        Id = id;
        PersonId = personId;
        InformationType = ınformationType;
        InformationContent = informationContent;
    }

    public Guid Id { get; set; }
    public Guid PersonId { get; set; }
    public InformationTypes InformationType { get; set; }
    public string InformationContent { get; set; }

    public virtual Person Person { get; set; }

    public static ContactInformation Create(Guid personId, InformationTypes informationType, string informationContent)
    {
        ContactInformation contactInformation = new ContactInformation(Guid.NewGuid(), personId, informationType, informationContent);

        return contactInformation;
    }
}