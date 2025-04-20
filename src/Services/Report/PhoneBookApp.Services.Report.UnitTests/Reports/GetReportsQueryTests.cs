using FluentAssertions;
using NSubstitute;
using PhoneBookApp.Services.Report.Application.Reports.GetReports;
using PhoneBookApp.Services.Report.Domain.Reports;

namespace PhoneBookApp.Services.Report.UnitTests.Reports;

public class GetReportsQueryTests
{
    private readonly GetReportsQueryHandler _handler;
    private readonly IReportRepository _reportRepositoryMock;

    public GetReportsQueryTests()
    {
        _reportRepositoryMock = Substitute.For<IReportRepository>();

        _handler = new GetReportsQueryHandler(_reportRepositoryMock);
    }

    [Fact]
    public async void Handle_Should_ReturnSuccess_WhenSomeReportExists()
    {
        // Arrange
        var report = Domain.Reports.Report.Create(DateTime.UtcNow, ReportStatuses.Preparing);

        var getReports = new List<GetReportsDto>
        {
            new GetReportsDto
            {
                Id = report.Id,
                RequestDate = report.RequestDate,
                ReportStatus = report.ReportStatus
            }
        };

        var getReportsQuery = new GetReportsQuery();

        _reportRepositoryMock
            .GetListAsync()
            .ReturnsForAnyArgs(new List<Domain.Reports.Report> { report });

        // Act
        var result = await _handler.Handle(getReportsQuery, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(getReports);
    }
}
