using BLL.BusinessObjects;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Converters
{
    public class ReceiptConverter
    {
        private CustomerConverter _custConv;

        private EmployeeConverter _empConv;

        public ReceiptConverter()
        {
            _custConv = new CustomerConverter();
            _empConv = new EmployeeConverter();
        }

        public Receipt Convert(ReceiptBO receipt)
        {
            if (receipt == null) { return null; }
            {
                return new Receipt()
                {
                    Id = receipt.Id,
                    Title = receipt.Title,
                    Description = receipt.Description,
                    CreationDate = receipt.CreationDate,
                    Customer = _custConv.Convert(receipt.Customer),
                    CustomerId = receipt.CustomerId,
                    Employee = _empConv.Convert(receipt.Employee),
                    EmployeeId = receipt.EmployeeId,
                    FileId = receipt.FileId
                };
            }
        }

        public ReceiptBO Convert(Receipt receipt)
        {
            if (receipt == null) { return null; }
            return new ReceiptBO()
            {
                Id = receipt.Id,
                Title = receipt.Title,
                Description = receipt.Description,
                CreationDate = receipt.CreationDate,
                Customer = _custConv.Convert(receipt.Customer),
                CustomerId = receipt.CustomerId,
                Employee = _empConv.Convert(receipt.Employee),
                EmployeeId = receipt.EmployeeId,
                FileId = receipt.FileId
            };
        }
    }
}

