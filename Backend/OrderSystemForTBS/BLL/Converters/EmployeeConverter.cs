using System;
using System.Collections.Generic;
using System.Text;
using BLL.BusinessObjects;
using DAL.Entities;

namespace BLL.Converters
{
    public class EmployeeConverter
    {
        public Employee Convert(EmployeeBO employee)
        {
            if (employee == null) { return null; }
            {
                return new Employee()
                {
                    Id = employee.Id,
                    Firstname = employee.Firstname,
                    Lastname = employee.Lastname,
                    Username = employee.Username,
                    Password = employee.Password,
                    PasswordReset = employee.PasswordReset,
                    PasswordHash = employee.PasswordHash,
                    PasswordSalt = employee.PasswordSalt,
                    ColorCode = employee.ColorCode,
                    IsAdmin = employee.IsAdmin
                 
                };
            }
        }

        public EmployeeBO Convert(Employee employee)
        {
            if (employee == null) { return null; }
            return new EmployeeBO()
            {
                Id = employee.Id,
                Firstname = employee.Firstname,
                Lastname = employee.Lastname,
                Username = employee.Username,
                Password = employee.Password,
                PasswordReset = employee.PasswordReset,
                PasswordHash = employee.PasswordHash,
                PasswordSalt = employee.PasswordSalt,
                ColorCode = employee.ColorCode,
                IsAdmin = employee.IsAdmin
            };
        }
    }
}
