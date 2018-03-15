using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Context;
using DAL.Entities;
using DAL.IRepositories;

namespace DAL.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public OrderSystemContext _context;

        public EmployeeRepository(OrderSystemContext context)
        {
            _context = context;
        }

        public Employee Create(Employee ent)
        {
            _context.Employees.Add(ent);
            return ent;
        }

        public Employee Delete(int Id)
        {
            var employee = Get(Id);
            _context.Employees.Remove(employee);
            return employee;

        }

        public Employee Get(int Id)
        {
            return _context.Employees.FirstOrDefault(x => x.Id == Id);
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }
    }
}


