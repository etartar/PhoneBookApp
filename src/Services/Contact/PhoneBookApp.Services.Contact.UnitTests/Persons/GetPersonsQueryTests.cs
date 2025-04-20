using FluentAssertions;
using NSubstitute;
using PhoneBookApp.Services.Contact.Application.Persons.GetPersons;
using PhoneBookApp.Services.Contact.Domain.Persons;

namespace PhoneBookApp.Services.Contact.UnitTests.Persons;

public class GetPersonsQueryTests
{
    private readonly GetPersonsQueryHandler _handler;
    private readonly IPersonRepository _personRepositoryMock;

    public GetPersonsQueryTests()
    {
        _personRepositoryMock = Substitute.For<IPersonRepository>();

        _handler = new GetPersonsQueryHandler(_personRepositoryMock);
    }

    [Fact]
    public async void Handle_Should_ReturnSuccess_WhenSomePersonExists()
    {
        // Arrange
        var person = Person.Create(PersonData.Name, PersonData.Surname, PersonData.CompanyName);

        var getPersons = new List<GetPersonsDto>
        {
            new GetPersonsDto
            {
                Id = person.Id,
                Name = person.Name,
                Surname = person.Surname,
                CompanyName = person.CompanyName
            }
        };

        var getPersonsQuery = new GetPersonsQuery();

        _personRepositoryMock
            .GetListAsync()
            .ReturnsForAnyArgs(new List<Person> { person });

        // Act
        var result = await _handler.Handle(getPersonsQuery, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(getPersons);
    }
}
