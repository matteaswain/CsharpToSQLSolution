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


            var vnd = sqllib.GetAllVendors(); // calls the data and stores into var

            var users = sqllib.GetAllUsers(); // calls the data and stores into var

            var user = sqllib.GetByPK(1); // calls the data and stores into var


            sqllib.Disconnect(); // calling the Disconn method
        }
    }
}
