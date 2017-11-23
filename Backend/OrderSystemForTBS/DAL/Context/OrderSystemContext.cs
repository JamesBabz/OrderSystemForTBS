using System.Data.Entity.ModelConfiguration.Conventions;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context
{
    public class OrderSystemContext : DbContext
    {


        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer(
                //    @"Server=tcp:eksamen.database.windows.net,1433;Initial Catalog=OrderSystem;Persist Security Info=False;
                //User ID=eksamen;Password=Admin123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

                optionsBuilder.UseSqlServer(
                    @"Server = tcp:databasetest1.database.windows.net, 1433; Initial Catalog = TestDb; Persist Security Info = False; User ID =test; Password =Jegersej10; 
                    MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;");
            }
        }



       
        }
}