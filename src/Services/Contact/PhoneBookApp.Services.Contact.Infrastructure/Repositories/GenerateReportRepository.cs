using Microsoft.EntityFrameworkCore;
using PhoneBookApp.Services.Contact.Domain.Reports;
using PhoneBookApp.Services.Contact.Infrastructure.Database;
using PhoneBookApp.Shared.Dtos;

namespace PhoneBookApp.Services.Contact.Infrastructure.Repositories;

internal sealed class GenerateReportRepository : IGenerateReportRepository
{
    private readonly ContactDbContext DbContext;

    public GenerateReportRepository(ContactDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<List<ReportDetailDto>> GetContactReports(CancellationToken cancellationToken = default)
    {
        return await DbContext.Database.SqlQuery<ReportDetailDto>($@"
                SELECT 
	                ci.information_content AS location, 
	                COUNT(distinct p.id) AS total_person_count,
	                COUNT(distinct cid.information_content) AS total_phone_number_count
                FROM 
	                contact_informations AS ci 
	                INNER JOIN persons p ON p.id = ci.person_id 
	                INNER JOIN (select information_content, person_id from contact_informations where information_type = 1) AS cid ON cid.person_id = ci.person_id
                WHERE 
	                ci.information_type = 3
                GROUP BY 
	                ci.information_content
            ").ToListAsync(cancellationToken);
    }
}
