using System;
using System.Collections.Generic;
using System.Text;
using BLL.BusinessObjects;

namespace BLL.IServices
{
    public interface IEmployeeService
    {
        //C
        EmployeeBO Create(EmployeeBO employee);
        //R
        EmployeeBO Get(int id);
        List<EmployeeBO> GetAll();
        //U
        EmployeeBO Update(EmployeeBO employee);
        //D
        EmployeeBO Delete(int id);
    }
}
