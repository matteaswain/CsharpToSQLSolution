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

        public List<Product> GetAll()
        {
            var sql = "Select * from Products ;";
            var cmd = new SqlCommand(sql, connection.sqlconn);
            var sqldatareader = cmd.ExecuteReader();
            var products = new List<Product>();

            while (sqldatareader.Read())
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

                products.Add(product);
            }
            sqldatareader.Close();

            foreach(var product in products)
            {
                GetVendorForProduct(product);
            }
            return products;
        }

        private void GetVendorForProduct(Product product)
        {
            var vendCtrl = new VendorsController(connection);
            product.Vendor = vendCtrl.GetByPK(product.VendorId);
        }






    }
}
