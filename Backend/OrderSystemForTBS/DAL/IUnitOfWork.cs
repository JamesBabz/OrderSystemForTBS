using System;
using DAL.Context;
using DAL.Entities;
using DAL.IRepositories;
using DAL.Repositories;

namespace DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Customer> CustomerRepository { get; }
        IRepository<Employee> EmployeeRepository { get; }
        IPropositionRepository PropositionRepository { get; }
        IRepository<Equipment> EquipmentRepository { get; }
        IVisitRepository VisitRepository { get; }
        IFilePathRepository FilePathRepository { get; }
        int Complete();
    }
}
