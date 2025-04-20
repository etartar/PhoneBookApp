using Mapster;
using PhoneBookApp.Services.Report.Domain.ReportDetails;

namespace PhoneBookApp.Services.Report.Application.Reports.GetReport;

public static class GetReportMapping
{
    public static void GetReportQueryMapping()
    {
        TypeAdapterConfig<Domain.Reports.Report, GetReportDto>
            .NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.RequestDate, src => src.RequestDate)
            .Map(dest => dest.ReportStatus, src => src.ReportStatus)
            .Map(dest => dest.Details, src => src.ReportDetails);

        TypeAdapterConfig<ReportDetail, GetReportDetailDto>
            .NewConfig()
            .Map(dest => dest.Location, src => src.Location)
            .Map(dest => dest.TotalPersonCount, src => src.TotalPersonCount)
            .Map(dest => dest.TotalPhoneNumberCount, src => src.TotalPhoneNumberCount);
    }
}
