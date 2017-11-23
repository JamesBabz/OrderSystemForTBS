using DAL.Context;
using DAL.Entities;
using DAL.Repositories;

namespace DAL.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRepository<Customer> CustomerRepository { get; internal set; }


        public OrderSystemtContext context;
        //private static DbContextOptions<CustomerProjectContext> optionsStatic;

        public UnitOfWork(DbOptions opt)
        {
            context = new OrderSystemtContext();

            CustomerRepository = new CustomerRepository(context);
          
  
            
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
