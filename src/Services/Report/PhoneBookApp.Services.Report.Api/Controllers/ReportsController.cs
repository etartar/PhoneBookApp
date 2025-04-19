using MediatR;
using Microsoft.AspNetCore.Mvc;
using PhoneBookApp.Core.Domain;
using PhoneBookApp.Core.Presentation.Results;
using PhoneBookApp.Services.Report.Application.Reports.CreateReport;
using PhoneBookApp.Services.Report.Application.Reports.GetReport;
using PhoneBookApp.Services.Report.Application.Reports.GetReports;

namespace PhoneBookApp.Services.Report.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ReportsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IResult> GetReports()
    {
        Result<List<GetReportsDto>> result = await mediator.Send(new GetReportsQuery());

        return result.Match(Results.Ok, ApiResults.Problem);
    }

    [HttpGet]
    public async Task<IResult> GetReport([FromQuery] Guid id)
    {
        Result<GetReportDto> result = await mediator.Send(new GetReportByIdQuery(id));

        return result.Match(Results.Ok, ApiResults.Problem);
    }

    [HttpPost]
    public async Task<IResult> CreateReport()
    {
        Result<Guid> result = await mediator.Send(new CreateReportCommand());

        return result.Match(Results.Ok, ApiResults.Problem);
    }
}
