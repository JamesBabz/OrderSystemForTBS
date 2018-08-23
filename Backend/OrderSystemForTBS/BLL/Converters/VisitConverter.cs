using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Converters
{
    using BLL.BusinessObjects;

    using DAL.Entities;

    public class VisitConverter
    {
        private CustomerConverter _custConv;

        private EmployeeConverter _empConv;

        public VisitConverter()
        {
            _custConv = new CustomerConverter();
            _empConv = new EmployeeConverter();

        }

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
                               Canceled = visit.Canceled,
                               ProgressPart = visit.ProgressPart,
                               DateTimeOfVisitStart = visit.DateTimeOfVisitStart,
                               DateTimeOfVisitEnd = visit.DateTimeOfVisitEnd,
                               CustomerId = visit.CustomerId,
                               Customer = _custConv.Convert(visit.Customer),
                               EmployeeId = visit.EmployeeId,
                               Employee = _empConv.Convert(visit.Employee)
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
                           Canceled = visit.Canceled,
                           ProgressPart = visit.ProgressPart,
                           DateTimeOfVisitStart = visit.DateTimeOfVisitStart,
                           DateTimeOfVisitEnd = visit.DateTimeOfVisitEnd,
                           CustomerId = visit.CustomerId,
                           Customer = _custConv.Convert(visit.Customer),
                           EmployeeId = visit.EmployeeId,
                           Employee = _empConv.Convert(visit.Employee)
                       };
        }
    }
}