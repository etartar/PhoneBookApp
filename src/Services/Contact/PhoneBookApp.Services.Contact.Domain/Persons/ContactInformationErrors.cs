using PhoneBookApp.Core.Domain;

namespace PhoneBookApp.Services.Contact.Domain.Persons;

public static class ContactInformationErrors
{
    public static Error NotFound(Guid contactInformation) => Error.NotFound(
        "ContactInformations.NotFound",
        $"Id = '{contactInformation}' olan iletişim bilgisi sistemde bulunamadı.");
}
