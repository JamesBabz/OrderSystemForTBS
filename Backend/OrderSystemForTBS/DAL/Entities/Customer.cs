using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{

    public class Customer : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
    }
}
