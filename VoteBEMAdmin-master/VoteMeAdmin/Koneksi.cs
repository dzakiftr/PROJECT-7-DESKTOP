using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace VoteMeAdmin
{
    class Koneksi
    {
        public SqlConnection getConnection()
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = "Data Source=DELL-PC; initial catalog=VoteBEM; user=sa; password=1234567890";
                return conn;
            }catch(Exception e)
            {
                Console.WriteLine("gagal " +e);
                return null;
            }
        }
    }
}
