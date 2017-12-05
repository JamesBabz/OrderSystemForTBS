using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;

namespace BLL.BusinessObjects
{
    public class EquipmentBO
    {
        public int id { get; set; }
        public string name { get; set; }

        public CustomerBO customer { get; set; }
        public int customerId { get; set; }
    }
}
