using BLL.BusinessObjects;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Converters
{
   public class SalesmanListConverter
    {
        private CustomerConverter _custConv;

        private EmployeeConverter _empConv;

        public SalesmanListConverter()
        {
            _custConv = new CustomerConverter();
            _empConv = new EmployeeConverter();
        }

        public SalesmanList Convert(SalesmanListBO BO)
        {
            if (BO == null) { return null; }
            {
                return new SalesmanList()
                {
                    Id = BO.Id,
                    EmployeeId = BO.EmployeeId,
                    CustomerId = BO.CustomerId,
                    Employee = _empConv.Convert(BO.Employee),
                    Customer = _custConv.Convert(BO.Customer)
                };
            }
        }

        public SalesmanListBO Convert(SalesmanList ent)
        {
            if (ent == null) { return null; }
            return new SalesmanListBO()
            {
                Id = ent.Id,
                EmployeeId = ent.EmployeeId,
                CustomerId = ent.CustomerId,
                Employee = _empConv.Convert(ent.Employee),
                Customer = _custConv.Convert(ent.Customer)
            };
        }




    }
}

