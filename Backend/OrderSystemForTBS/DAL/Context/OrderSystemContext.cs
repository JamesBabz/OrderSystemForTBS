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

            public OrderSystemContext(DbContextOptions<OrderSystemContext> options) : base(options)
            {
            }
            /*
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                if (!optionsBuilder.IsConfigured)
                {
                    optionsBuilder.UseInMemoryDatabase("Database");
                    optionsBuilder.UseSqlServer(
                        @"Server=tcp:eksamen.database.windows.net,1433;Initial Catalog=OrderSystem;Persist Security Info=False;
                    User ID=eksamen;Password=Admin123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

                }
            }
            */


       
        }
}