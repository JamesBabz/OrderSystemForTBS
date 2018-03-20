using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.BusinessObjects;
using BLL.Converters;
using BLL.IServices;
using DAL;
using DAL.Entities;

namespace BLL.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IDALFacade _facade;

        private EmployeeConverter _employeeConverter;
        private Employee _newEmployee;

        public EmployeeService(IDALFacade facade)
        {
            _employeeConverter = new EmployeeConverter();
            _facade = facade;
        }

        
        /// <summary>
        /// Create an employee with password hash and salt
        /// </summary>
        /// <param name="employee"> EmployeeBO to create</param>
        /// <returns> new EmployeeBO</returns>
        public EmployeeBO Create(EmployeeBO employee)
        {
            string password;
            byte[] passwordHash, passwordSalt;
            using (var uow = _facade.UnitOfWork)
            {
                password = employee.Password;
                _newEmployee = uow.EmployeeRepository.Create(_employeeConverter.Convert(employee));
                PasswordHash.CreatePasswordHash(password, out passwordHash, out passwordSalt);
                _newEmployee.PasswordHash = passwordHash;
                _newEmployee.PasswordSalt = passwordSalt;
                uow.Complete();
                return _employeeConverter.Convert(_newEmployee);
            }
        }

        public EmployeeBO Get(int Id)
        {
            using (var uow = _facade.UnitOfWork)
            {
                _newEmployee = uow.EmployeeRepository.Get(Id);
                uow.Complete();
                return _employeeConverter.Convert(_newEmployee);
            }
        }

        public List<EmployeeBO> GetAll()
        {
            using (var uow = _facade.UnitOfWork)
            {
                return uow.EmployeeRepository.GetAll().Select(_employeeConverter.Convert).ToList();
            }
        }

        //TODO implement
        public EmployeeBO Update(EmployeeBO bo)
        {
            throw new NotImplementedException();
        }

        // TODO implement 
        public EmployeeBO Delete(int Id)
        {
            throw new NotImplementedException();
        }

    }
}
