using FluentValidation;

namespace PhoneBookApp.Services.Contact.Application.ContactInformations.DeleteContactInformation;

public sealed class DeleteContactInformationCommandValidator : AbstractValidator<DeleteContactInformationCommand>
{
    public DeleteContactInformationCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}