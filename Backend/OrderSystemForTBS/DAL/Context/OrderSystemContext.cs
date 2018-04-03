using System.Data.Entity.ModelConfiguration.Conventions;

using DAL.Entities;

using Microsoft.EntityFrameworkCore;

namespace DAL.Context
{
    public class OrderSystemContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Proposition> Propositions { get; set; }

        public DbSet<Equipment> Equipments { get; set; }

        public DbSet<Visit> Visits { get; set; }

        public DbSet<SalesmanList> SalesmanLists { get; set; }

        public OrderSystemContext(DbContextOptions<OrderSystemContext> options)
            : base(options)
        {
        }

    }
}