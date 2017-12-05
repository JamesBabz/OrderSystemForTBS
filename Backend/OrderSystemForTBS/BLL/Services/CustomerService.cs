using System;
using System.Collections.Generic;
using System.Linq;

using BLL.BusinessObjects;
using BLL.Converters;

using DAL;
using DAL.Entities;

namespace BLL.Services
{
    public class CustomerService : ICustomerService
    {
        private IDALFacade facade;

        private List<CustomerBO> allCusts = new List<CustomerBO>();

        private CustomerConverter custConv = new CustomerConverter();

        private Customer newCustomer;

        public CustomerService(IDALFacade facade)
        {
            this.facade = facade;
        }

        public CustomerBO Create(CustomerBO cust)
        {
            using (var uow = facade.UnitOfWork)
            {
                newCustomer = uow.CustomerRepository.Create(custConv.Convert(cust));
                uow.Complete();
                return custConv.Convert(newCustomer);
            }
        }

        public List<CustomerBO> GetAll()
        {
            using (var uow = facade.UnitOfWork)
            {
                return uow.CustomerRepository.GetAll().Select(custConv.Convert).OrderBy(cust => cust.Firstname)
                    .ToList();
            }
        }

        public CustomerBO Get(int id)
        {
            using (var uow = facade.UnitOfWork)
            {
                newCustomer = uow.CustomerRepository.Get(id);
                uow.Complete();
                return custConv.Convert(newCustomer);
            }
        }

        public List<CustomerBO> GetAllBySearchQuery(string query)
        {
            var queryList = query.Split(" ");

            using (var uow = facade.UnitOfWork)
            {
                return uow.CustomerRepository.GetAll()
                    .Where(
                        customer => customer.Firstname.ToUpper().Contains(query.ToUpper())
                                    || customer.Lastname.ToUpper().Contains(query.ToUpper())
                                    || customer.Phone.ToString().Contains(query)
                                    || customer.Address.ToUpper().Contains(query.ToUpper())
                                    || customer.Firstname.ToUpper().Contains(queryList.GetValue(0).ToString().ToUpper())
                                    && customer.Lastname.ToUpper().Contains(queryList.GetValue(1).ToString().ToUpper())
                                    || customer.Lastname.ToUpper().Contains(queryList.GetValue(0).ToString().ToUpper())
                                    && customer.Firstname.ToUpper()
                                        .Contains(queryList.GetValue(1).ToString().ToUpper()))
                    .Select(customer => this.custConv.Convert(customer)).OrderBy(customer => customer.Firstname)
                    .ToList();
            }
        }

        public CustomerBO Update(CustomerBO cust)
        {
            using (var uow = facade.UnitOfWork)
            {
                var customerFromDb = uow.CustomerRepository.Get(cust.Id);

                // customerFromDb.customerId = cust.customerId;
                customerFromDb.Firstname = cust.Firstname;
                customerFromDb.Lastname = cust.Lastname;
                customerFromDb.Address = cust.Address;
                customerFromDb.ZipCode = cust.ZipCode;
                customerFromDb.City = cust.City;
                customerFromDb.Email = cust.Email;
                customerFromDb.CVR = cust.CVR;
                customerFromDb.Phone = cust.Phone;
                uow.Complete();
                return custConv.Convert(customerFromDb);
            }
        }

        public CustomerBO Delete(int Id)
        {
            using (var uow = facade.UnitOfWork)
            {
                newCustomer = uow.CustomerRepository.Delete(Id);
                uow.Complete();
                return custConv.Convert(newCustomer);
            }
        }
    }
}