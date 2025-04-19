using FluentValidation;

namespace PhoneBookApp.Services.Contact.Application.Persons.CreatePerson;

public sealed class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
{
    public CreatePersonCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();

        RuleFor(x => x.Name).MaximumLength(255).WithMessage("Ad maximum 255 karakter olabilir.");

        RuleFor(x => x.Surname).NotEmpty();

        RuleFor(x => x.Surname).MaximumLength(255).WithMessage("Soyad maximum 255 karakter olabilir.");

        RuleFor(x => x.CompanyName).NotEmpty();

        RuleFor(x => x.CompanyName).MaximumLength(255).WithMessage("Firma adı maximum 255 karakter olabilir.");
    }
}
