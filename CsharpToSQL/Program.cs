using CSharpToSqlLib;
using System;

namespace CsharpToSQL
{
    class Program
    {
        static void Main(string[] args)
        {
            var sqllib = new SqlLib(); // new instance of your Library class
            sqllib.Connect(); // calling the Conn method 

            var newuser = new User() // instance of the user class
            {
               // the values needed for inserting new user
                Username = "Nani",
                Password = "nanabanana",
                Firstname = "Kanani",
                Lastname = "Swain",
                Phone = "5026001234",
                Email = "Zealouskid89@gmail.com",
                IsReviewer = true, // boolean c# needs true or false
                IsAdmin = false
            };
            var success = sqllib.Create(newuser); // checks success of insert 


            //var vnd = sqllib.GetAllVendors(); // calls the data and stores into var

            var users = sqllib.GetAllUsers(); // calls the data and stores into var

            //var user = sqllib.GetByPK(1); // calls the data and stores into var


            sqllib.Disconnect(); // calling the Disconn method
        }
    }
}
