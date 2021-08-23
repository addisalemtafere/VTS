using System.Data;

namespace Application.Contracts.Repositories
{
    public interface ISqlConnectionFactory
    {
        IDbConnection GetOpenConnection();
    }
}