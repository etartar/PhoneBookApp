using FluentValidation;

namespace PhoneBookApp.Services.Contact.Application.Persons.DeletePerson;

public sealed class DeletePersonCommandValidator : AbstractValidator<DeletePersonCommand>
{
    public DeletePersonCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
