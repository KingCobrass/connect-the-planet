using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Connect.Interface.Database
{
    public interface IDatabaseHandler
    {
        DbConnection CreateConnection();

        DbCommand CreateCommand(string commandText, CommandType commandType);

        DbParameter CreateParameter(IDbCommand command);
    }
}
