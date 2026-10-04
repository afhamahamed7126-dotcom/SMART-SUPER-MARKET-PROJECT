using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SMARTSUPERMARKET
{
    internal class DBConnect
    {
        private SqlConnection con = new SqlConnection(@"Data Source =.\SQLExpress01; Initial Catalog = SMARTSUPERDB; Integrated Security = True");

       public SqlConnection GetCon()
        {
            return con;
        }
        public void OpenCon()
        {
            if (con.State==ConnectionState.Closed)
            {
                con.Open();
            }
        }

        public void CloseCon()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    } 
}
