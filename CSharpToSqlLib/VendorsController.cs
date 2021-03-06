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

        private Vendor FillVendorForSqlRow(SqlDataReader sqldatareader)
        {
            var vendor = new Vendor() // creating a new instance of the class Vendors
            {
                // assigning data to variables
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
            return vendor; 
        }

        private void FillParameterForSql(SqlCommand cmd, Vendor vendor)
        {
            cmd.Parameters.AddWithValue("@Code", vendor.Code);
            cmd.Parameters.AddWithValue("@Name", vendor.Name);
            cmd.Parameters.AddWithValue("@Address", vendor.Address);
            cmd.Parameters.AddWithValue("@City", vendor.City);
            cmd.Parameters.AddWithValue("@State", vendor.State);
            cmd.Parameters.AddWithValue("@Zip", vendor.Zip);
            cmd.Parameters.AddWithValue("@Phone", vendor.Phone);
            cmd.Parameters.AddWithValue("@Email", vendor.Email);

        }

        public List<Vendor> GetAll() // new method 
        {
            var sql = "Select * from Vendors ;"; //var assigned to sql statement 
            var cmd = new SqlCommand(sql, connection.sqlconn); // assign sql conn to var
            var sqldatareader = cmd.ExecuteReader(); // executes the select statement and stores data into variable
            var vendors = new List<Vendor>(); // create a new list for users
// reader - acts as pointer and returns true or false pending on if there is row of datas. rows = true, no rows = false
            while (sqldatareader.Read())// while reader is reading ... 
            { // var = convert [column name in database]

                var vendor = FillVendorForSqlRow(sqldatareader);
                vendors.Add(vendor); // adding what is read into a list 
            }
            sqldatareader.Close(); // closes reader// only 1 reader can be open at a time
            return vendors; // return list of vendors 
        }

        public Vendor GetByCode(string code)
        {
            var sql = " SELECT * from Vendors Where code = @code; ";
            var cmd = new SqlCommand(sql, connection.sqlconn);
            cmd.Parameters.AddWithValue("@code", code); // creating variable for the only piece of data needed for method 
            var sqldatareader = cmd.ExecuteReader();

            if (!sqldatareader.HasRows)
            {

                sqldatareader.Close();
                return null;
            }

            sqldatareader.Read();

            var vendor = FillVendorForSqlRow(sqldatareader);          
            sqldatareader.Close();
            return vendor; 

        }


        public Vendor GetByPK(int id)
        {
            var sql = $"Select * from Vendors where Id = {id};";
            var cmd = new SqlCommand(sql, connection.sqlconn);
            cmd.Parameters.AddWithValue("@id", id); // creating variable for the only piece of data needed for method
            var sqldatareader = cmd.ExecuteReader();

            if (!sqldatareader.HasRows)
            {
                sqldatareader.Close();
                return null;
            }
            sqldatareader.Read();

            var vendor = FillVendorForSqlRow(sqldatareader);           
            sqldatareader.Close();
            return vendor;
        }

        public bool Create(Vendor vendor)
        {
            var sql = $" Insert into Vendors " +
                "(Code, Name, Address, City, State, Zip, Phone, Email) " +
                " VALUES " +
                "(@Code, @Name, @Address, @City, @State, @Zip, @Phone, @Email) ; ";

            var cmd = new SqlCommand(sql, connection.sqlconn);
            FillParameterForSql(cmd, vendor);
           
            var rowsAffected = cmd.ExecuteNonQuery();

            return (rowsAffected == 1);            
        }
        public bool Change(Vendor vendor)
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
            FillParameterForSql(cmd, vendor);
            var rowsAffected = cmd.ExecuteNonQuery();

            return (rowsAffected == 1);
        }

        public bool Delete(Vendor vendor)
        {
            var sql = " DELETE from Vendors where Id = @Id; ";
            var cmd = new SqlCommand(sql, connection.sqlconn);
            cmd.Parameters.AddWithValue("@Id", vendor.Id);
            var rowsAffected = cmd.ExecuteNonQuery();

            return (rowsAffected == 1);
        }

    }
}
