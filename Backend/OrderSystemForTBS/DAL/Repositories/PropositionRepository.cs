using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using DAL.Context;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    class PropositionRepository : IRepository<Proposition>
    {
        private OrderSystemContext _context;

        public PropositionRepository(OrderSystemContext context)
        {
            _context = context;
        }

        public Proposition Create(Proposition ent)
        {
            _context.Propositions.Add(ent);
            return ent;
        }

        public IEnumerable<Proposition> GetAll()
        {
            return _context.Propositions.Include(prop => prop.Customer).Include(prop => prop.Employee).OrderByDescending(x => x.CreationDate).ToList();
        }

        public IEnumerable<Proposition> GetAll(int id)
        {
            //return _context.Propositions.Include(prop => prop.Customer).ToList();
            //return _context.Propositions.Include(prop => prop.Customer).Include(prop => prop.Employee).OrderByDescending(x => x.CreationDate).ToList();
            return _context.Propositions.Include(prop => prop.Customer).Include(prop => prop.Employee).OrderByDescending(x => x.CreationDate).Where(x => x.CustomerId == id).ToList();
        }

        public Proposition Get(int id)
        {
            return _context.Propositions.FirstOrDefault(x => x.Id == id);
        }

        public Proposition Delete(int id)
        {
            throw new NotImplementedException();
        }

        //public IEnumerable<Proposition> GetAllPropositionsByCustomerId(int id)
        //{
        //    return _context.Propositions.Include(prop => prop.CustomerId).Include(prop => prop.EmployeeId).ToList();
        //}
    }
}
