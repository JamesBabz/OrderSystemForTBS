using System;
using System.Collections.Generic;
using System.Text;
using DAL.Context;
using DAL.Entities;

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
            throw new NotImplementedException();
        }

        public Proposition Get(int Id)
        {
            throw new NotImplementedException();
        }

        public Proposition Delete(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
