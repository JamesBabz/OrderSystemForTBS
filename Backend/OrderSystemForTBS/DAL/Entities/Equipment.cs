using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Equipment 
    {
        public int  Id { get; set; }
        public string Name { get; set; }

        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
    }
}
