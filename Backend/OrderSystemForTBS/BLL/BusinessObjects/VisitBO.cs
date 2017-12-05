using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.BusinessObjects
{
   public class VisitBO { 

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DateOfVisit { get; set; }

        public bool IsDone { get; set; }

        public CustomerBO Customer { get; set; }

        public int customerId { get; set; }

        public EmployeeBO Employee { get; set; }

        public int employeeId { get; set; }
    }
}
