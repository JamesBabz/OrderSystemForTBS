﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Proposition : IEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public int CustomerId { get; set; }

        public int EmployeeId { get; set; }

        public int FileId { get; set; }

    }
}
