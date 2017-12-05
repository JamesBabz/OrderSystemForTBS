using System;
using System.Collections.Generic;
using System.Text;
using BLL.BusinessObjects;
using DAL.Entities;

namespace BLL.Converters
{
    public class EquipmentConverter
    {
        CustomerConverter custConv = new CustomerConverter();

        public Equipment Convert(EquipmentBO equipment)
        {
            if (equipment == null) { return null; }
            {
                return new Equipment()
                {
                    Name = equipment.name,

                    Customer = custConv.Convert(equipment.customer),
                    CustomerId = equipment.customerId
                };
            }
        }

        public EquipmentBO Convert(Equipment equipment)
        {
            if (equipment == null) { return null; }
            {
                return new EquipmentBO()
                {
                    name = equipment.Name,

                    customer = custConv.Convert(equipment.Customer),
                    customerId = equipment.CustomerId

                };
            }
        }
    }
}
