using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;

namespace DAL.IRepositories
{
    public interface IEquipmentRepository
    { 
        //C
        Equipment Create(Equipment equipment);
        //R
        Equipment Get(int id);
        IEnumerable<Equipment> GetAllById(int id); // TODO new name
        //U
        //No Update for Repository, It will be the task of Unit of Work
        //D
        Equipment Delete(int id);

    }
}
