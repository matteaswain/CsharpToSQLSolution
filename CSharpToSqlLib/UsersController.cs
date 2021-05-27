using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpToSqlLib
{
   public class UsersController
   {
        private static Connection connection { get; set; }

        private User FillUserForSqlRow(SqlDataReader sqldatareader)
        {
            var user = new User()
            {

            Id = Convert.ToInt32(sqldatareader["Id"]),
            Username = Convert.ToString(sqldatareader["Username"]),
            Password = sqldatareader["Password"].ToString(),
            Firstname = sqldatareader["Firstname"].ToString(),
            Lastname = sqldatareader["Lastname"].ToString(),
            Phone = sqldatareader["Phone"].ToString(),
            Email = sqldatareader["Email"].ToString(),
            IsReviewer = Convert.ToBoolean(sqldatareader["IsReviewer"]),
            IsAdmin = Convert.ToBoolean(sqldatareader["IsAdmin"])
            };
            return user;
        }

        private void FillParameterForSql(SqlCommand cmd, User user)
        {
            cmd.Parameters.AddWithValue("@username", user.Username);
            cmd.Parameters.AddWithValue("@password", user.Password);
            cmd.Parameters.AddWithValue("@firstname", user.Firstname);
            cmd.Parameters.AddWithValue("@lastname", user.Lastname);
            cmd.Parameters.AddWithValue("@phone", user.Phone);
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@isreviewer", user.IsReviewer);
            cmd.Parameters.AddWithValue("@isadmin", user.IsAdmin);
        }


        public List<User> GetAll()
        {
            var sql = " Select * from Users; ";
            var cmd = new SqlCommand(sql, connection.sqlconn);
            var sqldatareader = cmd.ExecuteReader();
            var users = new List<User>();

            while (sqldatareader.Read())
            {



                var user = FillUserForSqlRow(sqldatareader);
                users.Add(user);
              
            }
            sqldatareader.Close();
            return users;
        }

        public User GetByPK(int id)
        {
            var sql = $" Select * from Users where Id = {id}; ";
            var cmd = new SqlCommand(sql, connection.sqlconn);
            cmd.Parameters.AddWithValue("@id", id);
            var sqldatareader = cmd.ExecuteReader();

            if (!sqldatareader.HasRows)
            {
                sqldatareader.Close();
                return null;
            }
            sqldatareader.Read();

            var user = FillUserForSqlRow(sqldatareader);
            sqldatareader.Close();
            return user; 
        }


        public bool Create(User user)
        {
            var sql = $"Insert into Users" + // sql statment used for insert 
               " (Username, Password, Firstname, Lastname, Phone, Email, IsReviewer, IsAdmin) " +
                " VALUES " +
                " (@username , @password , @firstname , @lastname , @phone, @email, @isreviewer , @isadmin); ";
            // @parameter allows for safer coding

            var cmd = new SqlCommand(sql, connection.sqlconn);
            FillParameterForSql(cmd, user);

            var rowsAffected = cmd.ExecuteNonQuery();

            return (rowsAffected == 1);

        }







    }
}
