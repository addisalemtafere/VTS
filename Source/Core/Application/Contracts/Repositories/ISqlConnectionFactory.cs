using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Repositories
{
    public interface ISqlConnectionFactory
    {
        IDbConnection GetOpenConnection();
    }
}