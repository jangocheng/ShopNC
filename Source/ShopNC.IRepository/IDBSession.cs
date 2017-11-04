using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNC.IRepository
{
    public partial interface IDBSession
    {
        int ExcuteSql(string sql, params SqlParameter[] parameters);
        List<T> SqlQuery<T>(string sql, params SqlParameter[] parameters);
        int SaveChanges();

        int SaveChagesAsync();

        void DisposeDBContext();
    }
}
