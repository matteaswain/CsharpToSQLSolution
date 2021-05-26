using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpToSqlLib
{
    public class VendorsController
    {
        private static Connection connection { get; set; }

        public VendorsController(Connection connection)
        {
            //class name + property = parameter // 
            VendorsController.connection = connection;
        }

        public List<Vendors> GetAll() // new method 
        {
            var sql = "Select * from Vendors ;"; //var assigned to sql statement 
            var cmd = new SqlCommand(sql, connection.sqlconn); // assign sql conn to var
            var sqldatareader = cmd.ExecuteReader(); // executes the select statement and stores data into variable
            var vendors = new List<Vendors>(); // create a new list for users
// reader - acts as pointer and returns true or false pending on if there is row of datas. rows = true, no rows = false
            while (sqldatareader.Read())// while reader is reading ... 
            { // var = convert [column name in database]
               
                var vendor = new Vendors() // creating an instance of class Vendor 
                {// assigning data to variables 
                    Id = Convert.ToInt32(sqldatareader["Id"]),
                    Code = Convert.ToString(sqldatareader["Code"]),
                    Name = Convert.ToString(sqldatareader["Name"]),
                    Address = Convert.ToString(sqldatareader["Address"]),
                    City = Convert.ToString(sqldatareader["City"]),
                    State = Convert.ToString(sqldatareader["State"]),
                    Zip = Convert.ToString(sqldatareader["Zip"]),
                    Phone = Convert.ToString(sqldatareader["Phone"]),
                    Email = Convert.ToString(sqldatareader["Email"])
                };
                vendors.Add(vendor); // adding what is read into a list 
            }
            sqldatareader.Close(); // closes reader// only 1 reader can be open at a time
            return vendors; // return list of vendors 
        }

        public Vendors GetByPK(int id)
        {
            var sql = $"Select * from Vendors where Id = {id};";
            var cmd = new SqlCommand(sql, connection.sqlconn);
            var sqldatareader = cmd.ExecuteReader();

            if (!sqldatareader.HasRows)
            {
                sqldatareader.Close();
                return null;
            }
            sqldatareader.Read();

            var vendor = new Vendors()
            {
                Id = Convert.ToInt32(sqldatareader["Id"]),
                Code = Convert.ToString(sqldatareader["Code"]),
                Name = Convert.ToString(sqldatareader["Name"]),
                Address = Convert.ToString(sqldatareader["Address"]), 
                City = Convert.ToString(sqldatareader["City"]),
                State = Convert.ToString(sqldatareader["State"]),
                Zip = Convert.ToString(sqldatareader["Zip"]),
                Phone = Convert.ToString(sqldatareader["Phone"]),
                Email = Convert.ToString(sqldatareader["Email"])
            };
            sqldatareader.Close();
            return vendor;
        }

        public bool Create(Vendors vendors)
        {
            var sql = $" Insert into Vendros " +
                "(Code, Name, Address, City, State, Zip, Phone, Email) " +
                " VALUES " +
                "(@Code, @Name, @Address, @City, @State, @Zip, @Phone, @Email) ; ";

            var cmd = new SqlCommand(sql, connection.sqlconn);
            cmd.Parameters.AddWithValue("@Code" , vendors.Code);
            cmd.Parameters.AddWithValue("@Name", vendors.Name);
            cmd.Parameters.AddWithValue("@Address", vendors.Address);
            cmd.Parameters.AddWithValue("@City", vendors.City);
            cmd.Parameters.AddWithValue("@State", vendors.State);
            cmd.Parameters.AddWithValue("@Zip", vendors.Zip);
            cmd.Parameters.AddWithValue("@Phone", vendors.Phone);
            cmd.Parameters.AddWithValue("@Email", vendors.Email);
            var rowsAffected = cmd.ExecuteNonQuery();

            return (rowsAffected == 1);            
        }

        public bool Change(Vendors vendors)
        {
            var sql = " UPDATE Vendors set " +
                " Code = @Code, " +
                " Name = @Name, " +
                " Address = @Address, " +
                " City = @City, " +
                " State = @State, " +
                " Zip = @Zip, " +
                " Phone = @Phone, " +
                " Email = @Email, " +
                "Where Id = @Id; ";

            var cmd = new SqlCommand(sql, connection.sqlconn);
            cmd.Parameters.AddWithValue("@Code", vendors.Code);
            cmd.Parameters.AddWithValue("@Name", vendors.Name);
            cmd.Parameters.AddWithValue("@Address", vendors.Address);
            cmd.Parameters.AddWithValue("@City", vendors.City);
            cmd.Parameters.AddWithValue("@State", vendors.State);
            cmd.Parameters.AddWithValue("@Zip", vendors.Zip);
            cmd.Parameters.AddWithValue("@Phone", vendors.Phone);
            cmd.Parameters.AddWithValue("@Email", vendors.Email);
            var rowsAffected = cmd.ExecuteNonQuery();

            return (rowsAffected == 1);
        }

        public bool Delete(Vendors vendors)
        {
            var sql = " DELETE from Vendors where Id = @Id; ";
            var cmd = new SqlCommand(sql, connection.sqlconn);
            cmd.Parameters.AddWithValue("@Id", vendors.Id);
            var rowsAffected = cmd.ExecuteNonQuery();

            return (rowsAffected == 1);
        }

    }
}
