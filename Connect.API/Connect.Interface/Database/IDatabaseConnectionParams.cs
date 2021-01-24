using System;
using System.Collections.Generic;
using System.Text;

namespace Connect.Interface.Database
{
    public interface IDatabaseConnectionParams
    {
        string GetConnectionString();
        string GetProvider();
    }
}
