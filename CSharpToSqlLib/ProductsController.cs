using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpToSqlLib
{
   public class ProductsController
    {     
        private static Connection connection { get; set;}

        public ProductsController(Connection connection)
        {
            ProductsController.connection = connection;
        }

        private Product FillProductFromSqlRow(SqlDataReader sqldatareader)
        {
            var product = new Product()
            {
                Id = Convert.ToInt32(sqldatareader["Id"]),
                PartNbr = Convert.ToString(sqldatareader["PartNbr"]),
                Name = Convert.ToString(sqldatareader["Name"]),
                Price = Convert.ToDecimal(sqldatareader["Price"]),
                Unit = Convert.ToString(sqldatareader["Unit"]),
                PhotoPath = Convert.ToString(sqldatareader["PhotoPath"]),
                VendorId = Convert.ToInt32(sqldatareader["VendorId"])
            };
            return product;
        }

        private void FillParameterForSql(SqlCommand cmd, Product product)
        {
            cmd.Parameters.AddWithValue("@PartNbr", product.PartNbr);
            cmd.Parameters.AddWithValue("@Name", product.Name);
            cmd.Parameters.AddWithValue("@Price", product.Price);
            cmd.Parameters.AddWithValue("@Unit", product.Unit);
            cmd.Parameters.AddWithValue("@PhotoPath", (object)product.PhotoPath ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@VendorId", product.VendorId);
        }


        public List<Product> GetAll()
        {
            var sql = "Select * from Products ;";
            var cmd = new SqlCommand(sql, connection.sqlconn);
            var sqldatareader = cmd.ExecuteReader();
            var products = new List<Product>();

            while (sqldatareader.Read())
            {
               var product = FillProductFromSqlRow(sqldatareader);
                products.Add(product);
            }
            sqldatareader.Close();

            foreach(var product in products)
            {
                GetVendorForProduct(product);
            }
            return products;
        }

        // get vendors for a list of products
        private void GetVendorForProducts(List<Product> products)
        {
            foreach(var product in products)
            {
                GetVendorForProduct(product);
            }
        }
        // gets vendor for single product
        private void GetVendorForProduct(Product product)
        {
            var vendCtrl = new VendorsController(connection);
            product.Vendor = vendCtrl.GetByPK(product.VendorId);
        }


        public Product GetByPK(int id)
        {
            var sql = $"Select * from Products where Id = {id}; ";
            var cmd = new SqlCommand(sql, connection.sqlconn);
            cmd.Parameters.AddWithValue("@Id", id);
            var sqldatareader = cmd.ExecuteReader();

            if(!sqldatareader.HasRows)
            {
                sqldatareader.Close();
                return null; 
            }
            sqldatareader.Read();
            var product = FillProductFromSqlRow(sqldatareader);
            sqldatareader.Close();
            return product;
        }

        // creating product by vendor code
        public bool Create (Product product, string VendorCode)
        {
            var vendCtrl = new VendorsController(connection);
            var vendor = vendCtrl.GetByCode(VendorCode);
            {
                product.VendorId = vendor.Id;
                return Create(product);
            }
        }
        // creating product by vendor id
        public bool Create(Product product)
        {
            var sql = $" Insert into Products " +
                " (PartNbr, Name, Price, Unit, PhotoPath, VendorId) " +
                " VALUES " +
                " (@PartNbr, @Name, @Price, @Unit, @PhotoPath, @VendorId) ; ";

            var cmd = new SqlCommand(sql, connection.sqlconn);
            FillParameterForSql(cmd, product);
            var rowsAffected = cmd.ExecuteNonQuery();

            return (rowsAffected == 1);
        }

        // update product by vendor id
        public bool Change( Product product)
        {
            var sql = " UPDATE Products set " +
                " PartNbr = @PartNbr, " +
                " Name = @Name, " +
                " Price = @Price, " +
                " Unit = @Unit, " +
                " PhotoPath = @PhotoPath, " +
                " VendorId = @VendorId, " +
                " Where Id = @Id ; ";

            var cmd = new SqlCommand(sql, connection.sqlconn);
            FillParameterForSql(cmd, product);
            var rowsAffected = cmd.ExecuteNonQuery();

            return (rowsAffected == 1);
        }

        // deleting product by vendor id
        public bool Delete (Product product)
        {

            var sql = " DELETE from Products wgere Id = @Id; ";
            var cmd = new SqlCommand(sql, connection.sqlconn);

            cmd.Parameters.AddWithValue("@Id", product.Id);
            var rowsAffected = cmd.ExecuteNonQuery();

            return (rowsAffected == 1);


        }

    }
}
