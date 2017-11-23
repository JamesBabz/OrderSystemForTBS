using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.BusinessObjects
{
    public class PropositionBO : IBusinessObject
    {
        public int Id { get; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public int CustomerId { get; set; }

        public int EmployeeId { get; set; }

        public int FileId { get; set; }
    }
}
