using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.BusinessObjects
{
    public class SalesmanListBO
    {
        public int Id { get; set; }
        public CustomerBO Customer { get; set; }
        public int CustomerId { get; set; }

        public EmployeeBO Employee { get; set; }
        public int EmployeeId { get; set; }

    }
}
