using System;
using System.Data.Entity;
using System.Transactions;
using BLL.BusinessObjects;
using BLL.Converters;
using BLL.Services;
using DAL;
using DAL.Context;
using DAL.Entities;
using DAL.Facade;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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
            var serv = new EmployeeService(new DALFacade(new DbOptions()));
            var employee = new EmployeeBO()
            {
                FirstName = "Bent",
                LastName = "Hansen",
                Username = "jh@tbs.dk",
                Password = "1234",
                MacAddress = "hej"
            };

            Assert.AreEqual(serv.Get(11).FirstName, employee.FirstName = "Bent");
        }

        //[TestMethod]
        //public void GetAllEmployees()
        //{
        //    var serv = new EmployeeService(new DALFacade(new DbOptions()));
        //    var employee = ne
        //}

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

            Assert.AreEqual(serv.Get(10).FirstName, employee.FirstName = "Jens");
        }
    }
}


