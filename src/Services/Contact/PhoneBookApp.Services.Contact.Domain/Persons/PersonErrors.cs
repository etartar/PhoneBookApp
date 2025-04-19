using PhoneBookApp.Core.Domain;

namespace PhoneBookApp.Services.Contact.Domain.Persons;

public static class PersonErrors
{
    public static Error NotFound(string personName) => Error.NotFound(
        "Persons.NotFound",
        $"'{personName}' adlı kişi sistemde bulunamadı.");

    public static Error NotFound(Guid personId) => Error.NotFound(
        "Persons.NotFound",
        $"Id = '{personId}' olan kişi sistemde bulunamadı.");
}
