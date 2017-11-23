using System;
using DAL.Context;
using DAL.Entities;
using DAL.Repositories;

namespace DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Customer> CustomerRepository { get; }
        IRepository<Employee> EmployeeRepository { get; }
        IRepository<Proposition> PropositionRepository { get; }
        IRepository<Employee> EmployeeRepository { get; }
        int Complete();
    }
}
