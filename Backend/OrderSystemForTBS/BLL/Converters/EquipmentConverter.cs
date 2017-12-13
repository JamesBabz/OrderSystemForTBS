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
                    Id = equipment.id,
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
                    id = equipment.Id,
                    name = equipment.Name,

                    customer = custConv.Convert(equipment.Customer),
                    customerId = equipment.CustomerId

                };
            }
        }
    }
}
