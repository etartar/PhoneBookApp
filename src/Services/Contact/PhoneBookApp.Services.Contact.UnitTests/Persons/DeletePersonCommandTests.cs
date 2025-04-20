using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using PhoneBookApp.Core.Application.Abstractions;
using PhoneBookApp.Services.Contact.Application.Persons.DeletePerson;
using PhoneBookApp.Services.Contact.Domain.Persons;
using System.Linq.Expressions;

namespace PhoneBookApp.Services.Contact.UnitTests.Persons;

public class DeletePersonCommandTests
{
    private readonly DeletePersonCommandHandler _handler;
    private readonly IPersonRepository _personRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;

    public DeletePersonCommandTests()
    {
        _personRepositoryMock = Substitute.For<IPersonRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();

        _handler = new DeletePersonCommandHandler(_personRepositoryMock, _unitOfWorkMock);
    }

    [Fact]
    public async void Handle_Should_ReturnFailure_WhenPersonIsDeleted()
    {
        // Arrange
        var person = Person.Create(PersonData.Name, PersonData.Surname, PersonData.CompanyName);

        var deletePersonCommand = new DeletePersonCommand(person.Id);

        _personRepositoryMock
            .GetAsync(x => x.Id == person.Id, cancellationToken: default)
            .ReturnsNull();

        _personRepositoryMock
            .Delete(person);

        _unitOfWorkMock
            .SaveChangesAsync(default)
            .Returns(1);

        // Act
        var result = await _handler.Handle(deletePersonCommand, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(PersonErrors.NotFound(person.Id));
    }

    [Fact]
    public async void Handle_Should_ReturnSuccess_WhenPersonIsDeleted()
    {
        // Arrange
        var person = Person.Create(PersonData.Name, PersonData.Surname, PersonData.CompanyName);

        var deletePersonCommand = new DeletePersonCommand(person.Id);

        _personRepositoryMock
            .GetAsync(x => x.Id == person.Id, cancellationToken: default)
            .ReturnsForAnyArgs(person);

        _personRepositoryMock
            .Delete(person);

        _unitOfWorkMock
            .SaveChangesAsync(default)
            .Returns(1);

        // Act
        var result = await _handler.Handle(deletePersonCommand, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}
