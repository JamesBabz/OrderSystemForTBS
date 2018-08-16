using System;
using System.Collections.Generic;
using System.Text;
using BLL.BusinessObjects;

namespace BLL.IServices
{
    public interface IEquipmentService
    {
        //C
        EquipmentBO Create(EquipmentBO equipment);
        //R
        EquipmentBO Get(int id);
        List<EquipmentBO> GetAllById(int id);

        //D
        EquipmentBO Delete(int id);
    }
}
