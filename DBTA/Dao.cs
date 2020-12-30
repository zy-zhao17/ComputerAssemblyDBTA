using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using System.Data.SqlClient;

namespace DBTA
{
    class Dao
    {
        SqlConnection sc;
        public SqlConnection connect()
        {
            string str = "Data Source = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 166.111.68.220)(PORT = 1521))(CONNECT_DATA = (SERVICE_NAME = dbta))); Persist Security Info = True; User ID = s2017011295; Password = 141592;";
            sc = new SqlConnection(str);
            sc.Open();
            return sc;

        }
        public SqlCommand command(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql,connect());
            return cmd;

        }
        public int Execute(string sql)
        {
            return command(sql).ExecuteNonQuery();
        }
        
        public SqlDataReader read(string sql)
        {
            return command(sql).ExecuteReader();
        }
        public void DaoClose()
        {
            sc.Close();
        }
    }
}
