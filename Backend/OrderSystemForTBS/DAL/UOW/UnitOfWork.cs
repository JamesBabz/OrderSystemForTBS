using DAL.Context;
using DAL.Entities;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRepository<Customer> CustomerRepository { get; internal set; }
        public IRepository<Employee> EmployeeRepository { get; internal set; }
        public IRepository<Proposition> PropositionRepository { get; internal set; }


        public OrderSystemContext context;
        //private static DbContextOptions<CustomerProjectContext> optionsStatic;

        public UnitOfWork(OrderSystemContext context)
        {
            this.context = context;
            CustomerRepository = new CustomerRepository(context);
            EmployeeRepository = new EmployeeRepository(context);
            PropositionRepository = new PropositionRepository(context);


            context.Database.EnsureCreated();
        }
        
        public int Complete()
        {
            //The number of objects written to the underlying database.
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();


        }
        

    }
}
