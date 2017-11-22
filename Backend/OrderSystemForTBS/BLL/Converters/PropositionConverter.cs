using System;
using System.Collections.Generic;
using System.Text;
using BLL.BusinessObjects;
using DAL.Entities;

namespace BLL.Converters
{
    class PropositionConverter
    {
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
                    CustomerId = prop.CustomerId,
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
                Title = prop.Title,
                Description = prop.Description,
                CreationDate = prop.CreationDate,
                CustomerId = prop.CustomerId,
                EmployeeId = prop.EmployeeId,
                FileId = prop.FileId
            };
        }
    }
}
