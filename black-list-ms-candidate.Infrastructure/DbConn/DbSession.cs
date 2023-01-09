using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace black_list_ms_candidate.Infrastructure.DbConn
{
    public sealed class DbSession : IDisposable
    {
        private Guid _id;
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }

        public DbSession()
        {
            _id = Guid.NewGuid();
            //Connection = new SqlConnection(Settings.ConnectionString);
            //string strCon = "server=127.0.0.1;port=3306;database=blackListDB;user=root;password=root;Persist Security Info=False; Connect Timeout=300";
            string strCon = "server=localhost;port=3306;database=blackListDB;user=root;password=root;Persist Security Info=False; Connect Timeout=300";
            Connection = new MySqlConnection(strCon);
            Connection.Open();
        }

        public void Dispose() => Connection?.Dispose();
    }
}
