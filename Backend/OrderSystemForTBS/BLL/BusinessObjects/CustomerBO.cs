using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.BusinessObjects
{
    public class CustomerBO : IBusinessObject
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public int CVR { get; set; }
    }
}
