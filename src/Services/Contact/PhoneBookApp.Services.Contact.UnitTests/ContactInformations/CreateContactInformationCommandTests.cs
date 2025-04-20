using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using PhoneBookApp.Core.Application.Abstractions;
using PhoneBookApp.Services.Contact.Application.ContactInformations.CreateContactInformation;
using PhoneBookApp.Services.Contact.Domain.Persons;
using PhoneBookApp.Services.Contact.UnitTests.Persons;

namespace PhoneBookApp.Services.Contact.UnitTests.ContactInformations;

public class CreateContactInformationCommandTests
{
    private readonly CreateContactInformationCommandHandler _handler;
    private readonly IPersonRepository _personRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;

    public CreateContactInformationCommandTests()
    {
        _personRepositoryMock = Substitute.For<IPersonRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();

        _handler = new CreateContactInformationCommandHandler(_personRepositoryMock, _unitOfWorkMock);
    }

    [Fact]
    public async void Handle_Should_ReturnSuccess_WhenContactInformationIsCreated()
    {
        // Arrange
        var person = Person.Create(PersonData.Name, PersonData.Surname, PersonData.CompanyName);
        var contactInformation = ContactInformation.Create(person.Id, InformationTypes.PhoneNumber, "05542249733");

        var createContactInformationCommand = new CreateContactInformationCommand(person.Id, contactInformation.InformationType, contactInformation.InformationContent);

        _personRepositoryMock
            .GetAsync(x => x.Id == person.Id, cancellationToken: default)
            .ReturnsForAnyArgs(person);

        _personRepositoryMock
            .CreateContactInformation(contactInformation)
            .Returns(Task.CompletedTask);

        _unitOfWorkMock
            .SaveChangesAsync(default)
            .Returns(1);

        // Act
        var result = await _handler.Handle(createContactInformationCommand, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async void Handle_Should_ReturnFailure_WhenContactInformationIsCreated()
    {
        // Arrange
        var person = Person.Create(PersonData.Name, PersonData.Surname, PersonData.CompanyName);
        var contactInformation = ContactInformation.Create(person.Id, InformationTypes.PhoneNumber, "05542249733");

        var createContactInformationCommand = new CreateContactInformationCommand(person.Id, contactInformation.InformationType, contactInformation.InformationContent);

        _personRepositoryMock
            .GetAsync(x => x.Id == person.Id, cancellationToken: default)
            .ReturnsNull();

        _personRepositoryMock
            .CreateContactInformation(contactInformation)
            .Returns(Task.CompletedTask);

        _unitOfWorkMock
            .SaveChangesAsync(default)
            .Returns(1);

        // Act
        var result = await _handler.Handle(createContactInformationCommand, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(PersonErrors.NotFound(person.Id));
    }
}
