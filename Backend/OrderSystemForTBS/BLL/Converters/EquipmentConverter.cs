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
                    Id = equipment.Id,
                    Name = equipment.Name,

                    Customer = custConv.Convert(equipment.Customer),
                    CustomerId = equipment.CustomerId
                };
            }
        }

        public EquipmentBO Convert(Equipment equipment)
        {
            if (equipment == null) { return null; }
            {
                return new EquipmentBO()
                {
                    Id = equipment.Id,
                    Name = equipment.Name,

                    Customer = custConv.Convert(equipment.Customer),
                    CustomerId = equipment.CustomerId

                };
            }
        }
    }
}
