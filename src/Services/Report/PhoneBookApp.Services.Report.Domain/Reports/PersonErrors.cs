using PhoneBookApp.Core.Domain;

namespace PhoneBookApp.Services.Report.Domain.Reports;

public static class ReportErrors
{
    public static Error NotFound(string reportName) => Error.NotFound(
        "Reports.NotFound",
        $"'{reportName}' adlı rapor sistemde bulunamadı.");

    public static Error NotFound(Guid reportId) => Error.NotFound(
        "Reports.NotFound",
        $"Id = '{reportId}' olan rapor sistemde bulunamadı.");
}
