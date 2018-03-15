using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;

namespace DAL.IRepositories
{
    public interface IEmployeeRepository
    { 
        //C
        Employee Create(Employee employee);
        //R
        Employee Get(int id);
        IEnumerable<Employee> GetAll();
        //U
        //No Update for Repository, It will be the task of Unit of Work
        //D
        Employee Delete(int id);

    }
}
