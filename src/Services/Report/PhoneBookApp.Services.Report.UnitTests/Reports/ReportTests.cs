using FluentAssertions;
using PhoneBookApp.Services.Report.Domain.Reports;
using PhoneBookApp.Services.Report.UnitTests.Abstractions;

namespace PhoneBookApp.Services.Report.UnitTests.Reports;

public class ReportTests : BaseTests
{
    [Fact]
    public void Create_Should_RaiseReportCreatedDomainEvent()
    {
        // Arrange
        var requestDate = DateTime.UtcNow;
        var reportStatus = ReportStatuses.Preparing;

        // Act
        var report = Domain.Reports.Report.Create(requestDate, reportStatus);

        // Assert
        var domainEvent = AssertDomainEventWasPublished<ReportCreatedDomainEvent>(report);

        domainEvent.ReportId.Should().Be(report.Id);
    }
}
