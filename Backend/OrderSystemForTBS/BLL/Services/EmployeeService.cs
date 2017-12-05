using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.BusinessObjects;
using BLL.Converters;
using DAL;
using DAL.Entities;

namespace BLL.Services
{
    public class EmployeeService : IService<EmployeeBO>
    {
        private IDALFacade facade;
        private EmployeeConverter employeeConverter = new EmployeeConverter();
        private Employee newEmployee;

        public EmployeeService(IDALFacade facade)
        {
            this.facade = facade;
        }

        public EmployeeBO Create(EmployeeBO employee)
        {
            string password;
            byte[] passwordHash, passwordSalt;
            using (var uow = facade.UnitOfWork)
            {
                password = employee.Password;
                newEmployee = uow.EmployeeRepository.Create(employeeConverter.Convert(employee));
                PasswordHash.CreatePasswordHash(password, out passwordHash, out passwordSalt);
                newEmployee.PasswordHash = passwordHash;
                newEmployee.PasswordSalt = passwordSalt;
                uow.Complete();
                return employeeConverter.Convert(newEmployee);
            }
        }

        public EmployeeBO Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public List<EmployeeBO> GetAllById(int customerId)
        {
            throw new NotImplementedException();
        }

        public EmployeeBO Get(int Id)
        {
            using (var uow = facade.UnitOfWork)
            {
                newEmployee = uow.EmployeeRepository.Get(Id);
                uow.Complete();
                return employeeConverter.Convert(newEmployee);
            }
        }

        public List<EmployeeBO> GetAll()
        {
            using (var uow = facade.UnitOfWork)
            {
                return uow.EmployeeRepository.GetAll().Select(employeeConverter.Convert).ToList();
            }
        }

        public EmployeeBO Update(EmployeeBO bo)
        {
            throw new NotImplementedException();
        }
    }
}
