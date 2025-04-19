using PhoneBookApp.Core.Application.Abstractions;
using PhoneBookApp.Core.Application.Clock;
using PhoneBookApp.Core.Application.Messaging;
using PhoneBookApp.Core.Domain;
using PhoneBookApp.Services.Report.Domain.Reports;

namespace PhoneBookApp.Services.Report.Application.Reports.CreateReport;

public sealed class CreateReportCommandHandler(IDateTimeProvider dateTimeProvider, IReportRepository reportRepository, IUnitOfWork unitOfWork) : ICommandHandler<CreateReportCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateReportCommand request, CancellationToken cancellationToken)
    {
        var createReport = Domain.Reports.Report.Create(dateTimeProvider.UtcNow, ReportStatuses.Preparing);

        await reportRepository.AddAsync(createReport, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return createReport.Id;
    }
}
