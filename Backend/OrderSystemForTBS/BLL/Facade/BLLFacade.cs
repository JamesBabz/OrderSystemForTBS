using System;

using BLL.Services;

using DAL;
using DAL.Facade;

using Microsoft.Extensions.Configuration;

namespace BLL.Facade
{
    public class BLLFacade : IBLLFacade
    {
        private IDALFacade _facade;

        public BLLFacade() => _facade = new DALFacade();

        public CustomerService CustomerService
        {
            get
            {
                return new CustomerService(_facade);
            }
        }

        public PropositionService PropositionService
        {
            get
            {
                return new PropositionService(_facade);
            }
        }

        public EmployeeService EmployeeService
        {
            get
            {
                return new EmployeeService(_facade);
            }
        }

        public EquipmentService EquipmentService
        {
            get
            {
                return new EquipmentService(_facade);
            }
        }

        public VisitService VisitService
        {
            get
            {
                return new VisitService(_facade);
            }
        }

        public DawaService DawaService
        {
            get
            {
                return new DawaService();
            }
        }
        

        public CvrService CvrService
        {
            get { return new CvrService(); }
        }

        public SalesmanListService salesmanListService
        {
            get { return new SalesmanListService(_facade); }
        }

    }
}