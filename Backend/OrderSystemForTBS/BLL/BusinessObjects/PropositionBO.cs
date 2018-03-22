using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BLL.BusinessObjects
{
    public class PropositionBO 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }

        public CustomerBO Customer { get; set; }
        public int CustomerId { get; set; }

        public EmployeeBO Employee { get; set; }
        public int EmployeeId { get; set; }

        public long FileId { get; set; }
    }
}
