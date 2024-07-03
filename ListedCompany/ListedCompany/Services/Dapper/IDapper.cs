using Dapper;
using System.Data;

namespace ListedCompany.Services.Dapper;

public interface IDapper
{
    IEnumerable<TEntity> Get<TEntity>(string sp, DynamicParameters parms = null,
        CommandType commandType = CommandType.Text,
        string methodName = null, int cmdTimeout = 1200);
    IEnumerable<TEntity> GetAll<TEntity>(string sp, DynamicParameters parms = null,
        CommandType commandType = CommandType.Text,
        string methodName = null, int cmdTimeout = 1200);
    IEnumerable<TEntity> Execute<TEntity>(string sp, DynamicParameters parms = null,
        CommandType commandType = CommandType.StoredProcedure,
        string methodName = null, int cmdTimeout = 1200);
}
