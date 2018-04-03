using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class SalesmanList
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }

        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }


    }
}
