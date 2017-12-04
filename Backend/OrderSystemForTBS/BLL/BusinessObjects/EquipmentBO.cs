using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;

namespace BLL.BusinessObjects
{
    public class EquipmentBO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public CustomerBO Customer { get; set; }
        public int CustomerId { get; set; }
    }
}
