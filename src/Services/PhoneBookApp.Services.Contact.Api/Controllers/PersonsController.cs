using MediatR;
using Microsoft.AspNetCore.Mvc;
using PhoneBookApp.Core.Domain;
using PhoneBookApp.Core.Presentation.Results;
using PhoneBookApp.Services.Contact.Application.ContactInformations.CreateContactInformation;
using PhoneBookApp.Services.Contact.Application.ContactInformations.DeleteContactInformation;
using PhoneBookApp.Services.Contact.Application.Persons.CreatePerson;
using PhoneBookApp.Services.Contact.Application.Persons.DeletePerson;
using PhoneBookApp.Services.Contact.Application.Persons.GetPersonById;
using PhoneBookApp.Services.Contact.Application.Persons.GetPersons;

namespace PhoneBookApp.Services.Contact.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class PersonsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IResult> GetPersons()
    {
        Result<List<GetPersonsDto>> result = await mediator.Send(new GetPersonsQuery());

        return result.Match(Results.Ok, ApiResults.Problem);
    }

    [HttpGet]
    public async Task<IResult> GetPerson([FromQuery] Guid id)
    {
        Result<GetPersonDto> result = await mediator.Send(new GetPersonByIdQuery(id));

        return result.Match(Results.Ok, ApiResults.Problem);
    }

    [HttpPost]
    public async Task<IResult> CreatePerson([FromBody] CreatePersonRequest request)
    {
        Result<Guid> result = await mediator.Send(new CreatePersonCommand(request.Name, request.Surname, request.CompanyName));

        return result.Match(Results.Ok, ApiResults.Problem);
    }
    
    [HttpDelete]
    public async Task<IResult> DeletePerson([FromQuery] Guid id)
    {
        Result result = await mediator.Send(new DeletePersonCommand(id));

        return result.Match(Results.NoContent, ApiResults.Problem);
    }

    [HttpPost]
    public async Task<IResult> CreateContactInformation([FromBody] CreateContactInformationRequest request)
    {
        Result<Guid> result = await mediator.Send(new CreateContactInformationCommand(request.PersonId, request.InformationType, request.InformationContent));

        return result.Match(Results.Ok, ApiResults.Problem);
    }
    
    [HttpDelete]
    public async Task<IResult> DeleteContactInformation([FromQuery] Guid id)
    {
        Result result = await mediator.Send(new DeleteContactInformationCommand(id));

        return result.Match(Results.NoContent, ApiResults.Problem);
    }
}
