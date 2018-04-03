using System;

using DAL.Context;
using DAL.Entities;
using DAL.IRepositories;
using DAL.Repositories;

namespace DAL
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository CustomerRepository { get; }

        IEmployeeRepository EmployeeRepository { get; }

        IPropositionRepository PropositionRepository { get; }

        IEquipmentRepository EquipmentRepository { get; }

        IVisitRepository VisitRepository { get; }

        ISalesmanListRepository SalesmanListRepository { get; }

        int Complete();
    }
}