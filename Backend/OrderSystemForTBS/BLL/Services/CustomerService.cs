using BLL.BusinessObjects;
using BLL.Converters;
using DAL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services
{

    public class CustomerService : IService<CustomerBO>
    {
        private IDALFacade facade;
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
                return uow.CustomerRepository.GetAll().Select(custConv.Convert).ToList();
            }
        }

        public CustomerBO Get(int Id)
        {
            using (var uow = facade.UnitOfWork)
            {
                newCustomer = uow.CustomerRepository.Get(Id);
                uow.Complete();
                return custConv.Convert(newCustomer);
            }
        }

        public CustomerBO Update(CustomerBO cust)
        {
            using (var uow = facade.UnitOfWork)
            {
                var customerFromDb = uow.CustomerRepository.Get(cust.Id);
//                customerFromDb.Id = cust.Id;
                customerFromDb.Firstname = cust.Firstname;
                customerFromDb.Lastname = cust.Lastname;
                customerFromDb.Address = customerFromDb.Address;
                customerFromDb.ZipCode = cust.ZipCode;
                customerFromDb.City = cust.City;
                customerFromDb.Email = cust.City;
                customerFromDb.CVR = cust.CVR;
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
