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
        private IDALFacade _facade;

        private CustomerConverter _custConv;

        private Customer _newCustomer;

        public CustomerService(IDALFacade facade)
        {
            _facade = facade;
            _custConv = new CustomerConverter();
        }

        public CustomerBO Create(CustomerBO cust)
        {
            string firstName = cust.Firstname;
            firstName = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(firstName.ToLower());
            cust.Firstname = firstName;

            string lastName = cust.Lastname;
            lastName = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(lastName.ToLower());
            cust.Lastname = lastName;

            using (var uow = _facade.UnitOfWork)
            {
                _newCustomer = uow.CustomerRepository.Create(_custConv.Convert(cust));
                uow.Complete();
                return _custConv.Convert(_newCustomer);
            }
        }

        public List<CustomerBO> GetAll()
        {
            using (var uow = _facade.UnitOfWork)
            {
                return uow.CustomerRepository.GetAll().Select(_custConv.Convert).OrderBy(cust => cust.Firstname)
                    .ToList();
            }
        }

        public CustomerBO Get(int id)
        {
            using (var uow = _facade.UnitOfWork)
            {
                _newCustomer = uow.CustomerRepository.Get(id);
                uow.Complete();
                return _custConv.Convert(_newCustomer);
            }
        }

        // TODO Maybe a better way?
        public List<CustomerBO> GetAllBySearchQuery(string query)
        {
            var queryList = query.Split(" ");

            using (var uow = _facade.UnitOfWork)
            {
                return uow.CustomerRepository.GetAll() // static maybe??
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
                    .Select(customer => _custConv.Convert(customer)).OrderBy(customer => customer.Firstname)
                    .ToList();
            }
        }

        public CustomerBO Update(CustomerBO cust)
        {
            using (var uow = _facade.UnitOfWork)
            {
                var customerFromDb = uow.CustomerRepository.Get(cust.Id);

                customerFromDb.Firstname = cust.Firstname;
                customerFromDb.Lastname = cust.Lastname;
                customerFromDb.Address = cust.Address;
                customerFromDb.ZipCode = cust.ZipCode;
                customerFromDb.City = cust.City;
                customerFromDb.Email = cust.Email;
                customerFromDb.CVR = cust.CVR;
                customerFromDb.Phone = cust.Phone;
                customerFromDb.CompanyName = cust.CompanyName;
                customerFromDb.Description = cust.Description;
                uow.Complete();
                return _custConv.Convert(customerFromDb);
            }
        }

        public CustomerBO Delete(int Id)
        {
            using (var uow = _facade.UnitOfWork)
            {
                _newCustomer = uow.CustomerRepository.Delete(Id);
                uow.Complete();
                return _custConv.Convert(_newCustomer);
            }
        }
    }
}