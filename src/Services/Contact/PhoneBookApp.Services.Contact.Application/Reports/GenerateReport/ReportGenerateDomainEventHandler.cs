using MediatR;
using Microsoft.Extensions.Logging;
using PhoneBookApp.Core.Application.Exceptions;
using PhoneBookApp.Core.Application.Messaging;
using PhoneBookApp.Core.Domain;
using PhoneBookApp.Core.EventBus;
using PhoneBookApp.Services.Contact.Domain.Reports;
using PhoneBookApp.Shared.Dtos;
using PhoneBookApp.Shared.IntegrationEvents;

namespace PhoneBookApp.Services.Contact.Application.Reports.GenerateReport;

internal sealed class ReportGenerateDomainEventHandler(
    ILogger<ReportGenerateDomainEvent> logger,
    ISender sender,
    IEventBus eventBus) : DomainEventHandler<ReportGenerateDomainEvent>
{
    public override async Task Handle(ReportGenerateDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{Event} consumed.", nameof(ReportGenerateDomainEvent));

        Result<List<ReportDetailDto>> generatedReports = await sender.Send(new GenerateReportQuery());

        if (generatedReports.IsFailure)
        {
            throw new PhoneBookAppException(nameof(GenerateReportQuery), generatedReports.Error);
        }

        await eventBus.PublishAsync(
            new ReportGeneratedIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                domainEvent.ReportId,
                generatedReports.Value),
            cancellationToken);
    }
}
