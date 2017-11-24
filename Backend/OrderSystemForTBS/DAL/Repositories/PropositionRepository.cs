using System;
using System.Collections.Generic;
using System.Linq;
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
            return _context.Propositions.FirstOrDefault(x => x.Id == Id);
        }

        public Proposition Delete(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
