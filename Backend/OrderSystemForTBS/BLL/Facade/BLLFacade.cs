﻿using System;
using BLL.Services;
using DAL;
using DAL.Facade;
using Microsoft.Extensions.Configuration;

namespace BLL.Facade
{
    public class BLLFacade : IBLLFacade
    {
        private IDALFacade facade;

        public BLLFacade() => facade = new DALFacade(new DbOptions()
        {
            Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
        });

        public CustomerService CustomerService
        {
            get { return new CustomerService(facade); }
        }

        public PropositionService PropositionService
        {
            get { return new PropositionService(facade);}
        }
        
        public EmployeeService EmployeeService
         {
             get { return new EmployeeService(facade); }
         }

    }
}
