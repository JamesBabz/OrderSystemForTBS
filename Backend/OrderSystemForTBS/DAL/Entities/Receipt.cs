using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
   public class Receipt
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }

        public Customer Customer { get; set; }
        public int CustomerId { get; set; }

        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }

        public long FileId { get; set; }
    }
}
