using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using DAL.Context;
using DAL.Entities;
using DAL.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    class ReceiptRepository : IReceiptRepository
    {
        private OrderSystemContext _context;

        public ReceiptRepository(OrderSystemContext context)
        {
            _context = context;
        }

        public Receipt Create(Receipt ent)
        {
            _context.Receipts.Add(ent);
            return ent;
        }

        public Receipt Delete(int Id)
        {
            var receipt = Get(Id);
            _context.Receipts.Remove(receipt);
            return receipt;
        }

        public Receipt Get(int Id)
        {
            return _context.Receipts.FirstOrDefault(x => x.Id == Id);
        }

        public IEnumerable<Receipt> GetAll(int id)
        {
            return _context.Receipts.Include(prop => prop.Customer).Include(prop => prop.Employee).OrderByDescending(x => x.CreationDate).Where(x => x.CustomerId == id).ToList();
        }
    }
}
