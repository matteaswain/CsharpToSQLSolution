using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpToSqlLib
{
    /*Dependancy Injection 
     *  limited number of connections to Sql. There is a need for the controllers to have a single connection 
     *  not many. A construter is needed to help controll the connection. Sql connection can be passed through a single 
     *  constructur and accessed by other classes/controllers. This means that each controller could access the same
     *  connection by making an instance of the connection class and use when needed. 
     *  This also allows for changes to be made to the Connection class without any need to update any other classes. 
*/
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
