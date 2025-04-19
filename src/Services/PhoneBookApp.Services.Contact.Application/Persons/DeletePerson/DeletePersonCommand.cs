using PhoneBookApp.Core.Application.Messaging;

namespace PhoneBookApp.Services.Contact.Application.Persons.DeletePerson;

public sealed class DeletePersonCommand : ICommand
{
    public DeletePersonCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
