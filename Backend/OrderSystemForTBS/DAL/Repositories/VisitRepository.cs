using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    using System.Linq;

    using DAL.Context;
    using DAL.Entities;
    using DAL.IRepositories;

    public class VisitRepository : IVisitRepository
    {
        OrderSystemContext _context;

        public VisitRepository(OrderSystemContext context)
        {
            _context = context;
        }

        public Visit Create(Visit visit)
        {
            this._context.Visits.Add(visit);
            return visit;
        }

        public IEnumerable<Visit> GetAll()
        {
            return this._context.Visits.ToList();
        }

        public Visit Get(int id)
        {
            return _context.Visits.FirstOrDefault(x => x.Id == id);
        }

        public Visit Delete(int id)
        {
            var visit = Get(id);
            _context.Visits.Remove(visit);
            return visit;
        }

        public IEnumerable<Visit> GetAll(int id)
        {
            throw new NotImplementedException();
        }
    }
}
