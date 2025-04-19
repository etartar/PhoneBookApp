using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneBookApp.Core.Application.Abstractions;
using PhoneBookApp.Services.Report.Domain.ReportRequests;
using PhoneBookApp.Services.Report.Domain.Reports;

namespace PhoneBookApp.Services.Report.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ReportsController(IReportRequestRepository reportRequestRepository, IUnitOfWork unitOfWork) : ControllerBase
{
}
