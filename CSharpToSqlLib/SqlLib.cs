using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace CSharpToSqlLib
{
  public class SqlLib // dont make static 
    {
        public SqlConnection sqlconn { get; set; } // property for SQL Conn. 

        public List<Vendors> GetAllVendors() // new method 
        {
            var sql = "Select * from Vendors ;"; //var assigned to sql statement 
            var cmd = new SqlCommand(sql, sqlconn); // assign sql conn to var
            var sqldatareader = cmd.ExecuteReader(); // executes the select statement and stores data into variable
            var vendors = new List<Vendors>(); // create a new list for users

            while (sqldatareader.Read())// while reader is reading ... 
            { // var = convert [column name in database]
                var id = Convert.ToInt32(sqldatareader["Id"]);
                var code = Convert.ToString(sqldatareader["Code"]);
                var name = sqldatareader["name"].ToString();
                var address = sqldatareader["address"].ToString();
                var city = sqldatareader["City"].ToString();
                var state = sqldatareader["State"].ToString();
                var zip = sqldatareader["Zip"].ToString();
                var phone = sqldatareader["Phone"].ToString();
                var email = sqldatareader["Email"].ToString();

                var vendor = new Vendors() // creating an instance of class Vendor 
                {// assigning data to variables 
                    Id = id,
                    Code = code,
                    Name = name,
                    Address = address,
                    City = city,
                    State = state,
                    Zip = zip,
                    Phone = phone,
                    Email = email
                };
                vendors.Add(vendor); // adding what is read into a list 

            }
            sqldatareader.Close(); // closes reader// only 1 reader can be open at a time
            return vendors; // return the data of vender 


        }

            public User GetByPK(int id) //methods
        {
            var sql = $"Select * from Users where Id = {id};";
            var cmd = new SqlCommand(sql, sqlconn);
            var sqldatareader = cmd.ExecuteReader();

            if (!sqldatareader.HasRows)
            {
                sqldatareader.Close();
                return null;
            }
            sqldatareader.Read();

            var user = new User()
            {
                Id = Convert.ToInt32(sqldatareader["Id"]),
                Username = Convert.ToString(sqldatareader["Username"]),
                Password = Convert.ToString(sqldatareader["Password"]),
                Firstname = Convert.ToString(sqldatareader["Firstname"]),
                Lastname = Convert.ToString(sqldatareader["Lastname"]),
                Phone = Convert.ToString(sqldatareader["Phone"]),
                Email = Convert.ToString(sqldatareader["Email"]),
                IsReviewer = Convert.ToBoolean(sqldatareader["IsReviewer"]),
                IsAdmin = Convert.ToBoolean(sqldatareader["IsAdmin"])
            };
            sqldatareader.Close(); // closes the datareader// Only 1 reader open at a time
            return user; 
        }

        public List<User> GetAllUsers() // creating list of Users
        {
            var sql = "Select * from Users ;"; //var assigned to sql statement 
            var cmd = new SqlCommand(sql,sqlconn); // assign sql conn to var
            var sqldatareader = cmd.ExecuteReader(); // executes the select statement and stores data into variable
            var users = new List<User>(); // create a new list for users

            while (sqldatareader.Read())
            { // var = convert [column name in database]
                var id = Convert.ToInt32(sqldatareader["Id"]);
                var username = Convert.ToString(sqldatareader["Username"]);
                var password = sqldatareader["Password"].ToString();
                var firstname = sqldatareader["Firstname"].ToString();
                var lastname = sqldatareader["Lastname"].ToString();
                var phone = sqldatareader["Phone"].ToString();
                var email = sqldatareader["Email"].ToString();
                var isReviewer = Convert.ToBoolean(sqldatareader["IsReviewer"]);
                var isAdmin = Convert.ToBoolean(sqldatareader["IsAdmin"]);

                var user = new User() // creating a new instance of our User class
                {
                    Id = id,Username = username, Password = password,Firstname = firstname, Lastname = lastname,
                    Phone = phone, Email = email, IsReviewer = isReviewer, IsAdmin = isAdmin  // int all the vars
                };

                users.Add(user); // adding our users into our list 
            }
            sqldatareader.Close();
            return users; // returning list of users 
        }
        public void Connect() // create method to connect to SQL database
        {
            var connStr = "server=localhost\\sqlexpress;"+ // server = 'server location'\\ instance name;
                           "database=PrsDb;"+ // database = 'db name';
                           "trusted_connection=true;"; // sub for username/password
                                                       //
            sqlconn = new SqlConnection(connStr); // inalizing connection method
            sqlconn.Open(); // opens the connection to SQL 

            if(sqlconn.State != System.Data.ConnectionState.Open) // checks the connection worked
            {
                throw new Exception("Connection string is not correct!"); // blows up the system so can re-connect 
            }
            Console.WriteLine("Open connection successful!"); // advices connection was successful 
        }
        public void Disconnect() // method do disconnect from Db
        {
            if(sqlconn == null) // check sif connection was attempted
            {
                return; // if so can return nothing 
            }

            sqlconn.Close(); // disconnects from Db
            {
                sqlconn = null; // resets connection string to null 
            }
        }
    }
}
