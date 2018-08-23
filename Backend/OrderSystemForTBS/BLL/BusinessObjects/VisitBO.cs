using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.BusinessObjects
{
   public class VisitBO { 

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateTimeOfVisitStart { get; set; }
       public DateTime DateTimeOfVisitEnd { get; set; }
        public bool Canceled { get; set; }
        public int ProgressPart { get; set; }

        public CustomerBO Customer { get; set; }
        public int CustomerId { get; set; }

        public EmployeeBO Employee { get; set; }
        public int EmployeeId { get; set; }
    }
}
