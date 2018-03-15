using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;

namespace DAL.IRepositories
{
    public interface ICustomerRepository
    {
        //C
        Customer Create(Customer customer);
        //R
        Customer Get(int id);
        IEnumerable<Customer> GetAll();
        //U
        //No Update for Repository, It will be the task of Unit of Work
        //D
        Customer Delete(int id);

    }
}
