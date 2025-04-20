using PhoneBookApp.Core.Application.Messaging;

namespace PhoneBookApp.Services.Report.Application.Reports.GetReport;

public sealed class GetReportByIdQuery : IQuery<GetReportDto>
{
    public GetReportByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
