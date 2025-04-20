using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using PhoneBookApp.Services.Contact.Application.Persons.GetPersonById;
using PhoneBookApp.Services.Contact.Domain.Persons;

namespace PhoneBookApp.Services.Contact.UnitTests.Persons;

public class GetPersonByIdQueryTests
{
    private readonly GetPersonByIdQueryHandler _handler;
    private readonly IPersonRepository _personRepositoryMock;

    public GetPersonByIdQueryTests()
    {
        _personRepositoryMock = Substitute.For<IPersonRepository>();

        _handler = new GetPersonByIdQueryHandler(_personRepositoryMock);
    }

    [Fact]
    public async void Handle_Should_ReturnFailure_WhenPersonNotFound()
    {
        // Arrange
        var person = Person.Create(PersonData.Name, PersonData.Surname, PersonData.CompanyName);

        var getPersonByIdQuery = new GetPersonByIdQuery(person.Id);

        _personRepositoryMock
            .GetAsync(x => x.Id == person.Id, cancellationToken: default)
            .ReturnsNull();

        // Act
        var result = await _handler.Handle(getPersonByIdQuery, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(PersonErrors.NotFound(person.Id));
    }

    [Fact]
    public async void Handle_Should_ReturnSuccess_WhenPersonExists()
    {
        // Arrange
        var person = Person.Create(PersonData.Name, PersonData.Surname, PersonData.CompanyName);

        var getPerson = new GetPersonDto
        {
            Id = person.Id,
            Name = person.Name,
            Surname = person.Surname,
            CompanyName = person.CompanyName,
            ContactInformations = new List<GetContactInformationDto>()
        };

        var getPersonByIdQuery = new GetPersonByIdQuery(getPerson.Id);

        _personRepositoryMock
            .GetAsync(x => x.Id == person.Id, cancellationToken: default)
            .ReturnsForAnyArgs(person);

        // Act
        var result = await _handler.Handle(getPersonByIdQuery, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(getPerson);
    }
}
