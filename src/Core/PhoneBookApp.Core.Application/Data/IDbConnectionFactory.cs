using System.Data.Common;

namespace PhoneBookApp.Core.Application.Data;

public interface IDbConnectionFactory
{
    ValueTask<DbConnection> OpenConnectionAsync();
}
