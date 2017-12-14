using System;

using BLL.Services;

using DAL;
using DAL.Facade;

using Microsoft.Extensions.Configuration;

namespace BLL.Facade
{
    public class BLLFacade : IBLLFacade
    {
        private IDALFacade facade;

        public BLLFacade() => facade = new DALFacade(
                                  new DbOptions()
                                      {
                                          Environment =
                                              Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                                      });

        public CustomerService CustomerService
        {
            get
            {
                return new CustomerService(facade);
            }
        }

        public PropositionService PropositionService
        {
            get
            {
                return new PropositionService(facade);
            }
        }

        public EmployeeService EmployeeService
        {
            get
            {
                return new EmployeeService(facade);
            }
        }

        public EquipmentService EquipmentService
        {
            get
            {
                return new EquipmentService(facade);
            }
        }

        public VisitService VisitService
        {
            get
            {
                return new VisitService(this.facade);
            }
        }

        public DawaService DawaService
        {
            get
            {
                return new DawaService();
            }
        }
        
        public FilePathService FilePathService
        {
            get
            {
                return new FilePathService(this.facade);
            }
        }

<<<<<<< HEAD

        public CvrService CvrService
        {
            get
            {
                return new CvrService();
            }
=======
        public FileService FileService

        {
            get {return new FileService();}
>>>>>>> Development
        }
    }
}