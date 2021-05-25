using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpToSqlLib
{
   public class User // create class for table 'Users'
    {
        // properties reflect the columns from Users Table in SQL database
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsReviewer  { get; set; }
        public bool IsAdmin { get; set; }


    }
}
