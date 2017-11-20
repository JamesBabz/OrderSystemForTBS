using System.Data.Entity.ModelConfiguration.Conventions;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context
{
        public class OrderSystemtContext : DbContext
        {


        public DbSet<Customer> Customers { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    @"Server=tcp:ateamcustomer.database.windows.net,1433;Initial Catalog=ATeamCustomerDb;Persist Security Info=False;User ID=admn;
                    Password=Admin1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

            }
        }



       
        }
}