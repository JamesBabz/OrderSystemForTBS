﻿using BLL.BusinessObjects;
using DAL.Entities;

namespace BLL.Converters
{
    public class CustomerConverter
    {
        public Customer Convert(CustomerBO cust)
        {
            if (cust == null) { return null; }
            {
                return new Customer()
                {
                    Id = cust.Id,
                    Firstname = cust.Firstname,
                    Lastname = cust.Lastname,
                    Address = cust.Address,
                    ZipCode = cust.ZipCode,
                    City = cust.City,
                    Email = cust.Email,
                    Phone = cust.Phone,
                    CVR = cust.CVR

                };
            }
        }

        public CustomerBO Convert(Customer cust)
        {
            if (cust == null) { return null; }
            return new CustomerBO()
            {
                Id = cust.Id,
                Firstname = cust.Firstname,
                Lastname = cust.Lastname,
                Address = cust.Address,
                ZipCode = cust.ZipCode,
                City = cust.City,
                Email = cust.Email,
                Phone = cust.Phone,
                CVR = cust.CVR
            };
        }




    }
}
