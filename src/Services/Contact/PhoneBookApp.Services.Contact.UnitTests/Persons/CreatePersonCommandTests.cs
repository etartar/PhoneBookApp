using FluentAssertions;
using NSubstitute;
using PhoneBookApp.Core.Application.Abstractions;
using PhoneBookApp.Services.Contact.Application.Persons.CreatePerson;
using PhoneBookApp.Services.Contact.Domain.Persons;

namespace PhoneBookApp.Services.Contact.UnitTests.Persons;

public class CreatePersonCommandTests
{
    private static readonly CreatePersonCommand Command = new(PersonData.Name, PersonData.Surname, PersonData.CompanyName);
    private readonly CreatePersonCommandHandler _handler;
    private readonly IPersonRepository _personRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;

    public CreatePersonCommandTests()
    {
        _personRepositoryMock = Substitute.For<IPersonRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();

        _handler = new CreatePersonCommandHandler(_personRepositoryMock, _unitOfWorkMock);
    }

    [Fact]
    public async void Handle_Should_ReturnSuccess_WhenPersonIsCreated()
    {
        // Arrange
        var person = Person.Create(PersonData.Name, PersonData.Surname, PersonData.CompanyName);

        _personRepositoryMock
            .AddAsync(person)
            .Returns(Task.CompletedTask);

        _unitOfWorkMock
            .SaveChangesAsync(default)
            .Returns(1);

        // Act
        var result = await _handler.Handle(Command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}