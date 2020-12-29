using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace DBTA.Utils
{
    public class Connection
    {

        private static OracleConnection OpenConn()
        {
            OracleConnection conn = new OracleConnection();
            if (!System.IO.File.Exists("loginfo.txt")) throw new Exception(@"未找可用的数据库，请联系zy-zhao17@mails.tsinghua.edu.cn了解更多信息");
            conn.ConnectionString = System.IO.File.ReadAllText("loginfo.txt");
            conn.Open();
            return conn;
        }

        private static void CloseConn(OracleConnection conn)
        {
            if (conn == null) { return; }
            try
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Dispose();
            }
        }

        public static List<string> query(string cmdTxt)
        {
            List<string> ans = new List<string>();

            OracleConnection conn = null;
            try
            {
                conn = OpenConn();
                var cmd = conn.CreateCommand();
                cmd.CommandText = cmdTxt;
                cmd.CommandType = CommandType.Text;
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int len = reader.FieldCount;
                    for (int i = 0; i < len; i++)
                    {
                        ans.Add(reader[i].ToString());
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { CloseConn(conn); }
            return ans;
        } 
    }
}

