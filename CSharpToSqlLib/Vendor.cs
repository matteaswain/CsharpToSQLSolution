using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpToSqlLib
{
    public class Vendor // create class for table 'Vendors'
    { // propertiess relfect the column headers in table 'Vendors'
        public int Id { get; set; }
        public string Code { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

    }
}
