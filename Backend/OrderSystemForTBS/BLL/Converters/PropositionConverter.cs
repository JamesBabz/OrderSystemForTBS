using System;
using System.Collections.Generic;
using System.Text;
using BLL.BusinessObjects;
using DAL.Entities;

namespace BLL.Converters
{
    class PropositionConverter
    {
        private CustomerConverter custConv;

        private EmployeeConverter empConv;

        public PropositionConverter()
        {
            custConv = new CustomerConverter();
            empConv = new EmployeeConverter();
        }

        public Proposition Convert(PropositionBO prop)
        {
            if (prop == null) { return null; }
            {
                return new Proposition()
                {
                    Id = prop.Id,
                    Title = prop.Title,
                    Description = prop.Description,
                    CreationDate = prop.CreationDate,
                    Customer = custConv.Convert(prop.Customer),
                    CustomerId = prop.CustomerId,
                    Employee = empConv.Convert(prop.Employee),
                    EmployeeId = prop.EmployeeId,
                    FileId = prop.FileId
                };
            }
        }

        public PropositionBO Convert(Proposition prop)
        {
            if (prop == null) { return null; }
            return new PropositionBO()
            {
                Id = prop.Id,
                Title = prop.Title,
                Description = prop.Description,
                CreationDate = prop.CreationDate,
                Customer = custConv.Convert(prop.Customer),
                CustomerId = prop.CustomerId,
                Employee = empConv.Convert(prop.Employee),
                EmployeeId = prop.EmployeeId,
                FileId = prop.FileId
            };
        }
    }
}
