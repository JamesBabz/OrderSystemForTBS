﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Visit 
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DateOfVisit { get; set; }

        public bool IsDone { get; set; }

        public Customer Customer { get; set; }

        public int customerId { get; set; }

        public Employee Employee { get; set; }

        public int employeeId { get; set; }

    }
}