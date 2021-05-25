using CSharpToSqlLib;
using System;

namespace CsharpToSQL
{
    class Program
    {
        static void Main(string[] args)
        {
            var sqllib = new SqlLib(); // new instance of your class
            sqllib.Connect(); // calling the Conn method 


            var users = sqllib.GetAllUsers(); // calls the data and stores into var

            var user = sqllib.GetByPK(1);


            sqllib.Disconnect(); // calling the Disconn method
        }
    }
}
