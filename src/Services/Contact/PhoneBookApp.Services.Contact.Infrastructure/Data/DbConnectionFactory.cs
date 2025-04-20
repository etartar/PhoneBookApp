using Npgsql;
using PhoneBookApp.Core.Application.Data;
using System.Data.Common;

namespace PhoneBookApp.Services.Contact.Infrastructure.Data;

internal sealed class DbConnectionFactory(NpgsqlDataSource dataSource) : IDbConnectionFactory
{
    public async ValueTask<DbConnection> OpenConnectionAsync()
    {
        return await dataSource.OpenConnectionAsync();
    }
}
