using FluentAssertions;
using NSubstitute;
using PhoneBookApp.Core.Application.Abstractions;
using PhoneBookApp.Core.Application.Clock;
using PhoneBookApp.Services.Report.Application.Reports.CreateReport;
using PhoneBookApp.Services.Report.Domain.Reports;

namespace PhoneBookApp.Services.Report.UnitTests.Reports;

public class CreateReportCommandTests
{
    private readonly CreateReportCommandHandler _handler;
    private readonly IDateTimeProvider _dateTimeProviderMock;
    private readonly IReportRepository _reportRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;

    public CreateReportCommandTests()
    {
        _dateTimeProviderMock = Substitute.For<IDateTimeProvider>();
        _reportRepositoryMock = Substitute.For<IReportRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();

        _handler = new CreateReportCommandHandler(_dateTimeProviderMock, _reportRepositoryMock, _unitOfWorkMock);
    }

    [Fact]
    public async void Handle_Should_ReturnSuccess_WhenReportIsCreated()
    {
        // Arrange
        var report = Domain.Reports.Report.Create(_dateTimeProviderMock.UtcNow, ReportStatuses.Preparing);

        var createReportCommand = new CreateReportCommand();

        _reportRepositoryMock
            .AddAsync(report)
            .Returns(Task.CompletedTask);

        _unitOfWorkMock
            .SaveChangesAsync(default)
            .Returns(1);

        // Act
        var result = await _handler.Handle(createReportCommand, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}
