using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace CSharpToSqlLib
{
    public class SqlLib // dont make static 
    {
        public SqlConnection sqlconn { get; set; } // property for SQL Conn. 


        //public bool Create(User user)
        //// public allows access of method in other classes // want to retrun a booleaan expression// name of method ( type User, parameter user) 
        //{
        //    var sql = $"Insert into Users" + // sql statment used for insert 
        //       $" (Username, Password, Firstname, Lastname, Phone, Email, IsReviewer, IsAdmin) " +
        //        $" VALUES " +
        //        $" ('{user.Username}' , '{user.Password}' , '{user.Firstname}' , '{user.Lastname}' , " +
        //        $" '{user.Phone}' , '{user.Email}', {(user.IsReviewer ? 1 : 0)} , {(user.IsAdmin ? 1 : 0)} );";
        //    // {brackets} allow for the instance of info  
        //    var sqlcmd = new SqlCommand(sql, sqlconn); // create the command function
        //    var rowsAffected = sqlcmd.ExecuteNonQuery(); // creates the executer for sql 

        //    return (rowsAffected == 1); // return (booleanexpression) 
        //}



        public bool Delete(User user)
        {
            var sql = $"DELETE from Users " + // sql statment used for insert 
                " Where Id = @id; ";
            // @parameter allows for safer coding

            var sqlcmd = new SqlCommand(sql, sqlconn); // create the command function
            sqlcmd.Parameters.AddWithValue("@id", user.Id);
            var rowsAffected = sqlcmd.ExecuteNonQuery(); // creates the executer for sql 

            return (rowsAffected == 1); // return (booleanexpression) 
        }

        public bool Change(User user)
        {
            var sql = $"UPDATE Users Set" + // sql statment used for insert 
                " Username = @username, " +
                "Password = @password, " +
                "Firstname = @firstname, " +
                "Lastname = @lastname, " +
                "Phone = @phone, " +
                "Email = @email, " +
                "IsReviewer = @isreviewer, " +
                "IsAdmin = @isadmin " +
                "Where Id = @id;";
            // @parameter allows for safer coding

            var sqlcmd = new SqlCommand(sql, sqlconn); // create the command function
            sqlcmd.Parameters.AddWithValue("@username", user.Username);
            sqlcmd.Parameters.AddWithValue("@password", user.Password);
            sqlcmd.Parameters.AddWithValue("@firstname", user.Firstname);
            sqlcmd.Parameters.AddWithValue("@lastname", user.Lastname);
            sqlcmd.Parameters.AddWithValue("@phone", user.Phone);
            sqlcmd.Parameters.AddWithValue("@email", user.Email);
            sqlcmd.Parameters.AddWithValue("@isreviewer", user.IsReviewer);
            sqlcmd.Parameters.AddWithValue("@isadmin", user.IsAdmin);
            sqlcmd.Parameters.AddWithValue("@id", user.Id);
            var rowsAffected = sqlcmd.ExecuteNonQuery(); // creates the executer for sql 

            return (rowsAffected == 1); // return (booleanexpression) 
        }


        public bool Create(User user)
        // public allows access of method in other classes // want to retrun a booleaan expression// name of method ( type User, parameter user) 
        {
            var sql = $"Insert into Users" + // sql statment used for insert 
               " (Username, Password, Firstname, Lastname, Phone, Email, IsReviewer, IsAdmin) " +
                " VALUES " +
                " (@username , @password , @firstname , @lastname , @phone, @email, @isreviewer , @isadmin); ";
            // @parameter allows for safer coding

            var sqlcmd = new SqlCommand(sql, sqlconn); // create the command function
            sqlcmd.Parameters.AddWithValue("@username", user.Username);
            sqlcmd.Parameters.AddWithValue("@password", user.Password);
            sqlcmd.Parameters.AddWithValue("@firstname", user.Firstname);
            sqlcmd.Parameters.AddWithValue("@lastname", user.Lastname);
            sqlcmd.Parameters.AddWithValue("@phone", user.Phone);
            sqlcmd.Parameters.AddWithValue("@email", user.Email);
            sqlcmd.Parameters.AddWithValue("@isreviewer", user.IsReviewer);
            sqlcmd.Parameters.AddWithValue("@isadmin", user.IsAdmin);
            var rowsAffected = sqlcmd.ExecuteNonQuery(); // creates the executer for sql 

            return (rowsAffected == 1); // return (booleanexpression) 
        }

        public bool CreateMultiple(List<User> users)
        {
            var success = true;
            foreach (var user in users)
            {
                success = success && Create(user);
                // success is equal to success (true) AND if user is created = true, if not created = false
            }
            return success;
        }

        public User GetByPK(int id) //methods
        {
            var sql = $"Select * from Users where Id = {id};";
            var cmd = new SqlCommand(sql, sqlconn);
            var sqldatareader = cmd.ExecuteReader();// sql command 

            if (!sqldatareader.HasRows) // property 
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
            return user; // returning selected user
        }

        public List<User> GetAllUsers() // creating list of Users
        {
            var sql = "Select * from Users ;"; //var assigned to sql statement 
            var cmd = new SqlCommand(sql, sqlconn); // assign sql conn to var
            var sqldatareader = cmd.ExecuteReader(); // executes the select statement and stores data into variable
            var users = new List<User>(); // create a new list for users

            while (sqldatareader.Read()) // while reader is reading do : See below 
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
                {// Table column to var // Big to little
                    Id = id, Username = username, Password = password, Firstname = firstname, Lastname = lastname,
                    Phone = phone, Email = email, IsReviewer = isReviewer, IsAdmin = isAdmin  // int all the vars
                };

                users.Add(user); // adding our users into our list 
            }
            sqldatareader.Close();
            return users; // returning list of users 
        }
        public void Connect() // create method to connect to SQL database
        {
            var connStr = "server=localhost\\sqlexpress;" + // server = 'server location'\\ instance name;
                           "database=PrsDb;" + // database = 'db name';
                           "trusted_connection=true;"; // sub for username/password
                                                       //
            sqlconn = new SqlConnection(connStr); // inalizing connection method
            sqlconn.Open(); // opens the connection to SQL 

            if (sqlconn.State != System.Data.ConnectionState.Open) // checks the connection worked
            {
                throw new Exception("Connection string is not correct!"); // blows up the system so can re-connect 
            }
            Console.WriteLine("Open connection successful!"); // advices connection was successful 
        }
        public void Disconnect() // method do disconnect from Db
        {
            if (sqlconn == null) // check sif connection was attempted
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
