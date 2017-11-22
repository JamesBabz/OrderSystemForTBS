using System;
using System.Collections.Generic;
using System.Text;
using BLL.BusinessObjects;
using BLL.Converters;
using BLL.Services;
using DAL;
using DAL.Entities;
using DAL.Facade;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class UnitTestEmployee
    {
        private EmployeeConverter employeeConverter = new EmployeeConverter();

        [TestMethod]
        public void getEmployees()
        {
            
        }

        [TestMethod]
        public void createEmployee()
        {
            var serv = new EmployeeService(new DALFacade(new DbOptions()));
            var employee = new EmployeeBO()
            {
                FirstName = "Jens",
                LastName = "Hansen",
                Username = "jh@tbs.dk",
                Password = "1234",
                MacAddress = "hej"
            };

            serv.Create(employee);

            Assert.AreEqual(serv.Get(1).FirstName, employee.FirstName = "Jens");
        }


    }
}
