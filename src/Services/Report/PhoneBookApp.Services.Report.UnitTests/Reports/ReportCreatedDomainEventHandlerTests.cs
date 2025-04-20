using NSubstitute;
using PhoneBookApp.Core.EventBus;
using PhoneBookApp.Services.Report.Application.Reports.CreateReport;
using PhoneBookApp.Services.Report.Domain.Reports;
using PhoneBookApp.Shared.IntegrationEvents;

namespace PhoneBookApp.Services.Report.UnitTests.Reports;

public class ReportCreatedDomainEventHandlerTests
{
    private readonly ReportCreatedDomainEventHandler _handler;
    private readonly IEventBus _eventBusMock;

    public ReportCreatedDomainEventHandlerTests()
    {
        _eventBusMock = Substitute.For<IEventBus>();

        _handler = new ReportCreatedDomainEventHandler(_eventBusMock);
    }

    [Fact]
    public async Task Handle_ShouldPublishReportCreatedIntegrationEvent_WhenDomainEventIsHandled()
    {
        // Arrange
        var reportId = Guid.NewGuid();
        var domainEvent = new ReportCreatedDomainEvent(reportId);

        // Act
        await _handler.Handle(domainEvent);

        // Assert
        await _eventBusMock.Received(1).PublishAsync(Arg.Is<ReportCreatedIntegrationEvent>(e => e.ReportId == reportId));
    }
}
