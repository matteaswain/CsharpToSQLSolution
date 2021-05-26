using CSharpToSqlLib;
using System;

namespace CsharpToSQL
{
    class Program
    {
        static void Main(string[] args)
 // Concept: Dependency Injection \\ uses the constructor in the VC class to inject a need into another class
        {        // creating a connection placing in var sqlconn
            var sqlconn = new Connection("localhost\\sqlexpress","PrsDb"); 
  //creating new instance of VendorController / storing connetion into var vendorcontroller
            var vendorsController = new VendorsController(sqlconn); // 
            var vendors = vendorsController.GetAll();

            var productcntrol = new ProductsController(sqlconn);
            var products = productcntrol.GetAll();









            sqlconn.Disconnect();


           // var sqllib = new SqlLib(); // new instance of your Library class
           // sqllib.Connect(); // calling the Conn method 

           // //var newuser = new User() // instance of the user class
           // //{
           // //    // the values needed for inserting new user
           // //    Username = "Hotdog",
           // //    Password = "Bigdogontheblock",
           // //    Firstname = "Lucy",
           // //    Lastname = "Robb",
           // //    Phone = "5135557777",
           // //    Email = "Meatpaws@gmail.com",
           // //    IsReviewer = false, // boolean c# needs true or false
           // //    IsAdmin = false
           // //};
           //// var success = sqllib.Create(newuser); // checks success of insert
           // //var users = sqllib.GetAllUsers(); // calls the data and stores into var

           // //var user = sqllib.GetByPK(7); // calls the data and stores into var

           // //var vnd = sqllib.GetAllVendors(); // calls the data and stores into var
           // //sqllib.Delete(user); // using sqllib method Delete parameter user 

           // //user.Phone = "5026002007"; // changes to var user 2 above! 
           // //var success = sqllib.Delete(user); // change to user 
            

           // sqllib.Disconnect(); // calling the Disconn method
        }
    }
}
