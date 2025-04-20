namespace PhoneBookApp.Services.Report.Domain.ReportDetails;

public class ReportDetail
{
    public ReportDetail()
    {
    }

    public ReportDetail(Guid id, Guid reportId, string location, int totalPersonCount, int totalPhoneNumberCount)
    {
        Id = id;
        ReportId = reportId;
        Location = location;
        TotalPersonCount = totalPersonCount;
        TotalPhoneNumberCount = totalPhoneNumberCount;
    }

    public Guid Id { get; set; }
    public Guid ReportId { get; set; }
    public string Location { get; set; }
    public int TotalPersonCount { get; set; }
    public int TotalPhoneNumberCount { get; set; }

    public virtual Reports.Report Report { get; set; }

    public static ReportDetail Create(Guid reportId, string location, int totalPersonCount, int totalPhoneNumberCount)
    {
        return new ReportDetail(Guid.NewGuid(), reportId, location, totalPersonCount, totalPhoneNumberCount);
    }
}
