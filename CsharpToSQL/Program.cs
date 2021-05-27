using CSharpToSqlLib;
using System;
using System.Linq;

namespace CsharpToSQL
{
    class Program
    {
        static int[] ints = {
            505,916,549,881,918,385,350,228,489,719,
            866,252,130,706,581,313,767,691,678,187,
            115,660,653,564,805,720,729,392,598,791,
            620,345,292,318,726,501,236,573,890,357,
            854,212,670,782,267,455,579,849,229,661,
            611,588,703,607,824,730,239,118,684,149,
            206,952,531,809,134,929,593,385,520,214,
            643,191,998,555,656,738,829,454,195,419,
            326,996,666,242,189,464,553,579,188,884,
            197,369,435,476,181,192,439,615,746,277
        };
        static void Main(string[] args)
        {
            var sum = (from i in ints
                       where i % 7 == 0 || i % 11 == 0
                       select i).Sum();


// x is variable represents each iteam in collection, => lambda expression  
            var avg = ints.Where(x => x % 3 == 0 || x % 5 == 0).Average(); // LINQ method

            var avg0 = (from i in ints // LINQ query syntax 
                        where i % 3 == 0 || i % 5 == 0 
                        select i).Average();

            // var sum = 0;
            // var count = 0;
            //foreach(var i in ints)
            //{
            //     if (i % 3 == 0)
            //     {
            //         sum += i;
            //         count++;
            //     }
            // }

            // var avg = sum / count;


            // Concept: Dependency Injection \\ uses the constructor in the VC class to inject a need into another class
            // creating a connection placing in var sqlconn 
             var sqlconn = new Connection("localhost\\sqlexpress01","PrsDb");
            //creating new instance of VendorController / storing connetion into var vendorcontroller
            //var vendorsController = new VendorsController(sqlconn); // 
            //var vendors = vendorsController.GetAll();
            //// INSERTING A NEW VENDOR

            //var newvendor = new Vendor()
            //{
            //    Code = "KROG",
            //    Name = "Kroger",
            //    Address = "123 Krogie Street",
            //    City = "Reno",
            //    State = "NV",
            //    Zip = "54321",
            //    Phone = "8901976574",
            //    Email = "KrogerPlus@email.com"
            //};
            //var success = vendorsController.Create(newvendor);


            // INSERTING A NEW PRODUCT

            var productcntrol = new ProductsController(sqlconn);
            var products = productcntrol.GetAll();

            //var newproduct = new Product()
            //{
            //    PartNbr = "Kona",
            //    Name = "Kona Coffee",
            //    Price = 7,
            //    Unit = "Each",
            //    PhotoPath = null
            //};
            //var success = productcntrol.Create(newproduct, "KROG");

            //var product = productcntrol.GetByPK(2);




            //sqlconn.Disconnect();


           //var sqllib = new SqlLib(); // new instance of your Library class
           //  sqllib.Connect(); // calling the Conn method 
           // // INSERTING A NEW USER

           // var newuser = new User() // instance of the user class
           // {
           //     // the values needed for inserting new user
           //     Username = "Hotdog",
           //     Password = "Bigdogontheblock",
           //     Firstname = "Lucy",
           //     Lastname = "Robb",
           //     Phone = "5135557777",
           //     Email = "Meatpaws@gmail.com",
           //     IsReviewer = false, // boolean c# needs true or false
           //     IsAdmin = false
           // };
           // var success = sqllib.Create(newuser); // checks success of insert
           // var users = sqllib.GetAllUsers(); // calls the data and stores into var

            // //var user = sqllib.GetByPK(7); // calls the data and stores into var

            // //var vnd = sqllib.GetAllVendors(); // calls the data and stores into var
            // //sqllib.Delete(user); // using sqllib method Delete parameter user 

            // //user.Phone = "5026002007"; // changes to var user 2 above! 
            // //var success = sqllib.Delete(user); // change to user 


            /*sqllib.Disconnect();*/ // calling the Disconn method

            var productPriceAvg = (from p in products
                                   select p).Average(productPriceAvg => productPriceAvg.Price);

        }
    }
}
