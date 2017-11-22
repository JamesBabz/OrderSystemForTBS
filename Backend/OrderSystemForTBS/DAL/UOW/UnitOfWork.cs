using DAL.Context;
using DAL.Entities;
using DAL.Repositories;

namespace DAL.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRepository<Customer> CustomerRepository { get; internal set; }
        public IRepository<Employee> EmployeeRepository { get; internal set; }


        public OrderSystemContext context;
        //private static DbContextOptions<CustomerProjectContext> optionsStatic;

        public UnitOfWork(DbOptions opt)
        {
            context = new OrderSystemContext();

            CustomerRepository = new CustomerRepository(context);
            EmployeeRepository = new EmployeeRepository(context);

          
            context.Database.EnsureCreated();


            Employee employeeJens = new Employee()
            {
                Id = 1,
                FirstName = "Jens",
                LastName = "Hansen",
                MacAddress = "",
                Username = "jh@tbs.dk",
                Password = "1234"
            };


            context.Employees.Add(employeeJens);
            Complete();

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
