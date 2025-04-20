using MediatR;
using Microsoft.Extensions.Logging;
using PhoneBookApp.Core.Application.Exceptions;
using PhoneBookApp.Core.Domain;
using PhoneBookApp.Core.EventBus;
using PhoneBookApp.Shared.IntegrationEvents;

namespace PhoneBookApp.Services.Report.Application.Reports.CreateReportDetail;

public sealed class ReportGeneratedIntegrationEventHandler(ILogger<ReportGeneratedIntegrationEventHandler> logger, ISender sender) : IntegrationEventHandler<ReportGeneratedIntegrationEvent>
{
    public override async Task Handle(ReportGeneratedIntegrationEvent integrationEvent, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{Event} consumed.", nameof(ReportCreatedIntegrationEvent));

        Result generatedReportResult = await sender.Send(new CreateReportDetailCommand(
            integrationEvent.ReportId,
            integrationEvent.ReportDetails), cancellationToken);

        if (generatedReportResult.IsFailure)
        {
            throw new PhoneBookAppException(nameof(CreateReportDetailCommand), generatedReportResult.Error);
        }
    }
}
