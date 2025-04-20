using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using PhoneBookApp.Core.Application.Abstractions;
using PhoneBookApp.Core.Application.Messaging;
using PhoneBookApp.Core.Domain;
using PhoneBookApp.Services.Report.Domain.ReportDetails;
using PhoneBookApp.Services.Report.Domain.Reports;

namespace PhoneBookApp.Services.Report.Application.Reports.CreateReportDetail;

internal sealed class CreateReportDetailCommandHandler(
    ILogger<CreateReportDetailCommandHandler> logger,
    IReportRepository reportRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateReportDetailCommand>
{
    public async Task<Result> Handle(CreateReportDetailCommand request, CancellationToken cancellationToken)
    {
        using (IDbContextTransaction transaction = await unitOfWork.Database.BeginTransactionAsync(cancellationToken))
        {
            try
            {
                var getReport = await reportRepository.GetAsync(x => x.Id == request.ReportId, cancellationToken: cancellationToken);

                if (getReport is null)
                {
                    return Result.Failure(ReportErrors.NotFound(request.ReportId));
                }

                List<ReportDetail> createReportDetails = request.ReportDetails
                    .Select(x => new ReportDetail(
                        Guid.NewGuid(),
                        request.ReportId,
                        x.Location,
                        x.TotalPersonCount,
                        x.TotalPhoneNumberCount))
                    .ToList();

                await reportRepository.CreateReportDetails(createReportDetails, cancellationToken);

                getReport.ReportGenerated();

                reportRepository.Update(getReport);

                await unitOfWork.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                return Result.Success();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Transaction error: {message}", ex.Message);

                await transaction.RollbackAsync(cancellationToken);

                return Result.Failure(ReportErrors.TransactionError(ex.Message));
            }
        }
    }
}
