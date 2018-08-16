using DAL.Context;
using DAL.Entities;
using DAL.IRepositories;
using DAL.Repositories;

using Microsoft.EntityFrameworkCore;

namespace DAL.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICustomerRepository CustomerRepository { get; internal set; }

        public IEmployeeRepository EmployeeRepository { get; internal set; }

        public IPropositionRepository PropositionRepository { get; internal set; }

        public IEquipmentRepository EquipmentRepository { get; internal set; }

        public IVisitRepository VisitRepository { get; internal set; }

        public ISalesmanListRepository SalesmanListRepository { get; internal set; }

        public IReceiptRepository ReceiptRepository { get; internal set; }

        public OrderSystemContext context;

        public UnitOfWork(OrderSystemContext context)
        {
            this.context = context;
            this.CustomerRepository = new CustomerRepository(this.context);
            this.EmployeeRepository = new EmployeeRepository(this.context);
            this.PropositionRepository = new PropositionRepository(this.context);
            this.EquipmentRepository = new EquipmentRepository(this.context);
            this.VisitRepository = new VisitRepository(this.context);
            this.SalesmanListRepository = new SalesmanListRepository(this.context);
            this.ReceiptRepository = new ReceiptRepository(this.context);
            
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