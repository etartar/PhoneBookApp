using MongoDB.Bson.Serialization.Attributes;

namespace PhoneBookApp.Services.Report.Domain.ReportDetails;

public class ReportDetail
{
    public ReportDetail()
    {
    }

    public ReportDetail(Guid reportId, string location, int totalPersonCount, int totalPhoneNumberCount)
    {
        ReportId = reportId;
        Location = location;
        TotalPersonCount = totalPersonCount;
        TotalPhoneNumberCount = totalPhoneNumberCount;
    }

    public Guid Id { get; set; }

    [BsonElement("report_id")]
    public Guid ReportId { get; set; }

    [BsonElement("location")]
    public string Location { get; set; }

    [BsonElement("total_person_count")]
    public int TotalPersonCount { get; set; }

    [BsonElement("total_phone_number_count")]
    public int TotalPhoneNumberCount { get; set; }

    public virtual Reports.Report Report { get; set; }
}
