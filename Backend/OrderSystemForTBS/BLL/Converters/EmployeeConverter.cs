﻿using System;
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
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Username = employee.Username,
                    Password = employee.Password,
                    PasswordHash = employee.PasswordHash,
                    PasswordSalt = employee.PasswordSalt,
                    MacAddress = employee.MacAddress
                    
                };
            }
        }

        public EmployeeBO Convert(Employee employee)
        {
            if (employee == null) { return null; }
            return new EmployeeBO()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Username = employee.Username,
                Password = employee.Password,
                PasswordHash = employee.PasswordHash,
                PasswordSalt = employee.PasswordSalt,
                MacAddress = employee.MacAddress
            };
        }
    }
}
