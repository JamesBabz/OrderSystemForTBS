
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using DAL.Context;
using DAL.Entities;
using DAL.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class SalesmanListRepository : ISalesmanListRepository
    {
        OrderSystemContext _context;

        public SalesmanListRepository(OrderSystemContext context)
        {
            _context = context;
        }

        public SalesmanList Create(SalesmanList salesmanList)
        {
            _context.SalesmanLists.Add(salesmanList);
            return salesmanList;
        }

        public SalesmanList Delete(int Id)
        {
            var salesmanList = Get(Id);
            _context.SalesmanLists.Remove(salesmanList);
            return salesmanList;
        }

        public SalesmanList Get(int Id)
        {
            return _context.SalesmanLists.FirstOrDefault(x => x.Id == Id);
        }

        public IEnumerable<SalesmanList> GetAll()
        {
            return _context.SalesmanLists.Include(customer => customer.CustomerId).ToList();
        }

        public IEnumerable<SalesmanList> GetAllById(int Id)
        {
            return _context.SalesmanLists.Include(salesmanList => salesmanList.Employee).Include(salesmanList => salesmanList.Customer).Where(x => x.EmployeeId == Id).ToList();
        }
    }
}
