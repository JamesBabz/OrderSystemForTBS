using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Converters
{
    using BLL.BusinessObjects;

    using DAL.Entities;

    public class VisitConverter
    {
        CustomerConverter custConv = new CustomerConverter();

        EmployeeConverter empConv = new EmployeeConverter();

        public Visit Convert(VisitBO visit)
        {
            if (visit == null)
            {
                return null;
            }
            {
                return new Visit()
                           {
                               Id = visit.Id,
                               Title = visit.Title,
                               Description = visit.Description,
                               IsDone = visit.IsDone,
                               DateOfVisit = visit.DateOfVisit,
                               Customer = this.custConv.Convert(visit.Customer),
                               customerId = visit.customerId,
                               Employee = this.empConv.Convert(visit.Employee),
                               employeeId = visit.employeeId
                           };
            }
        }

        public VisitBO Convert(Visit visit)
        {
            if (visit == null)
            {
                return null;
            }

            return new VisitBO()
                       {
                           Id = visit.Id,
                           Title = visit.Title,
                           Description = visit.Description,
                           IsDone = visit.IsDone,
                           DateOfVisit = visit.DateOfVisit,
                           Customer = this.custConv.Convert(visit.Customer),
                           customerId = visit.customerId,
                           Employee = this.empConv.Convert(visit.Employee),
                           employeeId = visit.employeeId
                       };
        }
    }
}