using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpToSqlLib
{
    public class Connection
    {
        public SqlConnection sqlconn { get; set; } // property for SQL Conn. 

        public void Disconnect()
        {
            if(sqlconn == null)
            {
                return;
            }
            sqlconn.Close();
            sqlconn = null;
        }
        public Connection(string server, string database)
        {
// var for connection string = server = {var}; database = {var}; trusted_connection=true;
            var connStr = $"server={server};database={database};trusted_connection=true;";
            sqlconn = new SqlConnection(connStr); // var = new sqlconnection with connection string
            sqlconn.Open();
            if(sqlconn.State != System.Data.ConnectionState.Open)
            {
                sqlconn = null;
                throw new Exception("Connection failed. Try again");
            }
        }

    }
}
