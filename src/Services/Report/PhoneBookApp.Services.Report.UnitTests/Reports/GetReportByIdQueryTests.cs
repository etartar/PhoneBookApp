using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using PhoneBookApp.Services.Report.Application.Reports.GetReport;
using PhoneBookApp.Services.Report.Domain.Reports;

namespace PhoneBookApp.Services.Report.UnitTests.Reports;

public class GetReportByIdQueryTests
{
    private readonly GetReportByIdQueryHandler _handler;
    private readonly IReportRepository _reportRepositoryMock;

    public GetReportByIdQueryTests()
    {
        _reportRepositoryMock = Substitute.For<IReportRepository>();

        _handler = new GetReportByIdQueryHandler(_reportRepositoryMock);
    }

    [Fact]
    public async void Handle_Should_ReturnFailure_WhenReportNotFound()
    {
        // Arrange
        var report = Domain.Reports.Report.Create(DateTime.UtcNow, ReportStatuses.Preparing);

        var getReportByIdQuery = new GetReportByIdQuery(report.Id);

        _reportRepositoryMock
            .GetAsync(x => x.Id == report.Id, cancellationToken: default)
            .ReturnsNull();

        // Act
        var result = await _handler.Handle(getReportByIdQuery, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(ReportErrors.NotFound(report.Id));
    }

    [Fact]
    public async void Handle_Should_ReturnSuccess_WhenReportExists()
    {
        // Arrange
        var report = Domain.Reports.Report.Create(DateTime.UtcNow, ReportStatuses.Preparing);

        var getReport = new GetReportDto
        {
            Id = report.Id,
            RequestDate = report.RequestDate,
            ReportStatus = report.ReportStatus,
            Details = new List<GetReportDetailDto>()
        };

        var getReportByIdQuery = new GetReportByIdQuery(report.Id);

        _reportRepositoryMock
            .GetAsync(x => x.Id == report.Id, cancellationToken: default)
            .ReturnsForAnyArgs(report);

        // Act
        var result = await _handler.Handle(getReportByIdQuery, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(getReport);
    }
}
