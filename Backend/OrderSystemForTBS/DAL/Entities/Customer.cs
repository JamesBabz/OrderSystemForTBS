using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{

    public class Customer 
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public int CVR { get; set; }
    }
}
