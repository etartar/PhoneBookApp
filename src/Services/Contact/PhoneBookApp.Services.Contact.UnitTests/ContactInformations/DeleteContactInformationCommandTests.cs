using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using PhoneBookApp.Core.Application.Abstractions;
using PhoneBookApp.Services.Contact.Application.ContactInformations.CreateContactInformation;
using PhoneBookApp.Services.Contact.Application.ContactInformations.DeleteContactInformation;
using PhoneBookApp.Services.Contact.Domain.Persons;
using PhoneBookApp.Services.Contact.UnitTests.Persons;

namespace PhoneBookApp.Services.Contact.UnitTests.ContactInformations;

public class DeleteContactInformationCommandTests
{
    private readonly DeleteContactInformationCommandHandler _handler;
    private readonly IPersonRepository _personRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;

    public DeleteContactInformationCommandTests()
    {
        _personRepositoryMock = Substitute.For<IPersonRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();

        _handler = new DeleteContactInformationCommandHandler(_personRepositoryMock, _unitOfWorkMock);
    }

    [Fact]
    public async void Handle_Should_ReturnSuccess_WhenContactInformationIsDeleted()
    {
        // Arrange
        var person = Person.Create(PersonData.Name, PersonData.Surname, PersonData.CompanyName);
        var contactInformation = ContactInformation.Create(person.Id, InformationTypes.PhoneNumber, "05542249733");

        var deleteContactInformationCommand = new DeleteContactInformationCommand(contactInformation.Id);

        _personRepositoryMock
            .GetContactInformationAsync(x => x.Id == contactInformation.Id, cancellationToken: default)
            .ReturnsForAnyArgs(contactInformation);

        _personRepositoryMock
            .DeleteContactInformation(contactInformation);

        _unitOfWorkMock
            .SaveChangesAsync(default)
            .Returns(1);

        // Act
        var result = await _handler.Handle(deleteContactInformationCommand, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async void Handle_Should_ReturnFailure_WhenContactInformationIsDeleted()
    {
        // Arrange
        var person = Person.Create(PersonData.Name, PersonData.Surname, PersonData.CompanyName);
        var contactInformation = ContactInformation.Create(person.Id, InformationTypes.PhoneNumber, "05542249733");

        var deleteContactInformationCommand = new DeleteContactInformationCommand(contactInformation.Id);

        _personRepositoryMock
            .GetContactInformationAsync(x => x.Id == contactInformation.Id, cancellationToken: default)
            .ReturnsNull();

        _personRepositoryMock
            .DeleteContactInformation(contactInformation);

        _unitOfWorkMock
            .SaveChangesAsync(default)
            .Returns(1);

        // Act
        var result = await _handler.Handle(deleteContactInformationCommand, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(ContactInformationErrors.NotFound(contactInformation.Id));
    }
}
