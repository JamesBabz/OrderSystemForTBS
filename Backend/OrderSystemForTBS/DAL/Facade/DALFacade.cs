using DAL.Context;
using DAL.UOW;
using Microsoft.EntityFrameworkCore;

namespace DAL.Facade
{
    public class DALFacade : IDALFacade
    {
        DbOptions opt;

        public DALFacade(DbOptions opt){
            this.opt = opt;
        }

        public IUnitOfWork UnitOfWork
		{
			get
			{
			    var ob = new DbContextOptionsBuilder<OrderSystemContext>().UseSqlServer(
			        @"Server=tcp:eksamen.database.windows.net,1433;Initial Catalog=OrderSystem;Persist Security Info=False;
                    User ID=eksamen;Password=Admin123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
			    return new UnitOfWork(new OrderSystemContext(ob.Options));
			}
		}

    }
}
