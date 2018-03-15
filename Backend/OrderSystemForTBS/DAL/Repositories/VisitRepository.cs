using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    using System.Linq;

    using DAL.Context;
    using DAL.Entities;
    using DAL.IRepositories;

    using Microsoft.EntityFrameworkCore;

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

        public Visit Get(int id)
        {
            return _context.Visits.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Visit> GetAll()
        {
            return _context.Visits.Include(visit => visit.Customer).Include(visit => visit.Employee).ToList();
        }

        public Visit Delete(int id)
        {
            var visit = Get(id);
            _context.Visits.Remove(visit);
            return visit;
        }

        //new name
        public IEnumerable<Visit> GetAllById(int id)
        {
            return _context.Visits.Include(visit => visit.Customer).Include(visit => visit.Employee)
                .OrderByDescending(visit => visit.DateTimeOfVisitStart).Where(x => x.CustomerId == id).ToList();
        }
    }
}