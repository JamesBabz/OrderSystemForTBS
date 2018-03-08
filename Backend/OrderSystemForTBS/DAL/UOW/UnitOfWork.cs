﻿using DAL.Context;
using DAL.Entities;
using DAL.IRepositories;
using DAL.Repositories;

using Microsoft.EntityFrameworkCore;

namespace DAL.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRepository<Customer> CustomerRepository { get; internal set; }

        public IRepository<Employee> EmployeeRepository { get; internal set; }

        public IPropositionRepository PropositionRepository { get; internal set; }

        public IRepository<Equipment> EquipmentRepository { get; internal set; }

        public IVisitRepository VisitRepository { get; internal set; }

        public OrderSystemContext context;

        // private static DbContextOptions<CustomerProjectContext> optionsStatic;
        public UnitOfWork(OrderSystemContext context)
        {
            this.context = context;
            this.CustomerRepository = new CustomerRepository(this.context);
            this.EmployeeRepository = new EmployeeRepository(this.context);
            this.PropositionRepository = new PropositionRepository(this.context);
            this.EquipmentRepository = new EquipmentRepository(this.context);
            this.VisitRepository = new VisitRepository(this.context);

            // context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        public int Complete()
        {
            // The number of objects written to the underlying database.
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}