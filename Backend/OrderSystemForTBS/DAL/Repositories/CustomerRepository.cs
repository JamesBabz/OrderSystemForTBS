using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Context;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        OrderSystemContext _context;

        public CustomerRepository(OrderSystemContext context)
        {
            _context = context;
        }

        public Customer Create(Customer cust)
        {
            _context.Customers.Add(cust);
            return cust;
        }

        public IEnumerable<Customer> GetAll()
        {
            return _context.Customers.ToList();
        }


        public Customer Get(int Id)
        {
            return _context.Customers.FirstOrDefault(x => x.Id == Id);
        }

        public Customer Delete(int Id)
        {
            var customer = Get(Id);
            _context.Customers.Remove(customer);
            return customer;
        }
    }
}
