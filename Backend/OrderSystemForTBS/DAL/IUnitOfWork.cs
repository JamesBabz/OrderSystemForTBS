using System;
using DAL.Entities;
using DAL.Repositories;

namespace DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Customer> CustomerRepository { get; }

        int Complete();
    }
}
