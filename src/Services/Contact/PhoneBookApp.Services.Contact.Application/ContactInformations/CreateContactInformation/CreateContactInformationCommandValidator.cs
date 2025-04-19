using FluentValidation;

namespace PhoneBookApp.Services.Contact.Application.ContactInformations.CreateContactInformation;

public sealed class CreateContactInformationCommandValidator : AbstractValidator<CreateContactInformationCommand>
{
    public CreateContactInformationCommandValidator()
    {
        RuleFor(x => x.PersonId).NotEmpty();

        RuleFor(x => x.InformationType).IsInEnum();

        RuleFor(x => x.InformationContent).NotEmpty();
    }
}