﻿using System;
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
        private Employee userFromDb;
        private Boolean reset = false;

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
                 userFromDb = uow.EmployeeRepository.Get(emp.Id);

                Console.WriteLine(userFromDb.Password);
                Console.WriteLine(userFromDb.PasswordReset);

                if (emp.IsAdmin == "true")
                {
                    if (userFromDb.IsAdmin == "Administrator")
                    {
                        userFromDb.IsAdmin = "User";
                        userFromDb.Firstname = userFromDb.Firstname;
                        userFromDb.Lastname = userFromDb.Lastname;
                        userFromDb.ColorCode = userFromDb.ColorCode;
                    }
                    else if (userFromDb.IsAdmin == "User")
                    {
                        userFromDb.IsAdmin = "Administrator";
                        userFromDb.Firstname = userFromDb.Firstname;
                        userFromDb.Lastname = userFromDb.Lastname;
                        userFromDb.ColorCode = userFromDb.ColorCode;
                    }
                }

                if (userFromDb.PasswordReset && emp.Password != null)
                {
                    firstLogin(emp);
                }

                if (emp.Password == null && emp.IsAdmin == null)
                {
                    userFromDb.Firstname = emp.Firstname;
                    userFromDb.Lastname = emp.Lastname;
                    userFromDb.ColorCode = emp.ColorCode;
                }

                if (emp.PasswordReset)
                {
                    createPassword(emp);
                    _mailto.mailTo(userFromDb.Username, password, userFromDb.Firstname);
                    userFromDb.PasswordReset = true;
                }


                uow.Complete();
                return _employeeConverter.Convert(userFromDb);
            }
        }

        private void firstLogin(EmployeeBO emp)
        {
            userFromDb.Firstname = userFromDb.Firstname;
            userFromDb.Lastname = userFromDb.Lastname;
            userFromDb.ColorCode = userFromDb.ColorCode;
            userFromDb.Password = emp.Password;
            createPassword(emp);
            userFromDb.PasswordReset = false;
        }

        private void createPassword(EmployeeBO emp)
        {
            password = emp.Password;
            PasswordHash.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            userFromDb.PasswordHash = passwordHash;
            userFromDb.PasswordSalt = passwordSalt;
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
