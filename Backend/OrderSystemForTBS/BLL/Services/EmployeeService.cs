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

        string password;
        byte[] passwordHash, passwordSalt;
        private EmployeeConverter _employeeConverter;
        private Employee _newEmployee;
        private sendMail _mailto;

        public EmployeeService(IDALFacade facade)
        {
            _employeeConverter = new EmployeeConverter();
            _mailto = new sendMail();
            _facade = facade;
        }
        
        /// <summary>
        /// Create an employee with password hash and salt
        /// Sends mail with first login password
        /// </summary>
        /// <param name="employee"> EmployeeBO to create</param>
        /// <returns> new EmployeeBO</returns>
        public EmployeeBO Create(EmployeeBO employee)
        {
            
            using (var uow = _facade.UnitOfWork)
            {
                password = employee.Password;
                _newEmployee = uow.EmployeeRepository.Create(_employeeConverter.Convert(employee));
                PasswordHash.CreatePasswordHash(password, out passwordHash, out passwordSalt);
                _newEmployee.PasswordHash = passwordHash;
                _newEmployee.PasswordSalt = passwordSalt;
                _newEmployee.PasswordReset = true;
                _newEmployee.IsAdmin = "User";
               // _mailto.mailTo(_newEmployee.Username, password, _newEmployee.Firstname);
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

        public EmployeeBO Update(EmployeeBO emp)
        {
            using (var uow = _facade.UnitOfWork)
            {
                // gets prop from DB that matches the id
                var userFromDb = uow.EmployeeRepository.Get(emp.Id);
                userFromDb.Password = emp.Password;
                password = emp.Password;
                PasswordHash.CreatePasswordHash(password, out passwordHash, out passwordSalt);
                userFromDb.PasswordHash = passwordHash;
                userFromDb.PasswordSalt = passwordSalt;
                userFromDb.PasswordReset = false;
                uow.Complete();
                return _employeeConverter.Convert(userFromDb);
            }
        }

        
        public EmployeeBO Delete(int Id)
        {
            using (var uow = _facade.UnitOfWork)
            {
                _newEmployee = uow.EmployeeRepository.Get(Id);
                uow.EmployeeRepository.Delete(_newEmployee.Id);
                uow.Complete();
                return _employeeConverter.Convert(_newEmployee);
            }
        }

    }
}
