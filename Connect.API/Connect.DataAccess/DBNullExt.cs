using System;
using System.Collections.Generic;
using System.Text;

namespace Connect.DataAccess
{
    public static class DBNullExt
    {
        public static T ToValue<T>(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return default;
            else
                return (T)Convert.ChangeType(obj, typeof(T));

        }
    }
}
