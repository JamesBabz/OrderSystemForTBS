﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using DAL.Context;
using DAL.Entities;
using DAL.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    class PropositionRepository : IPropositionRepository
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
            var prop = Get(id);
            _context.Propositions.Remove(prop);
            return prop;
        }

        public List<int> getFileIds()
        {
            List<int> ids = new List<int>();
            foreach (var contextProposition in this._context.Propositions)
            {
                ids.Add(contextProposition.FileId);
            }
            ids.Sort();
            return ids;
        }

        //public IEnumerable<Proposition> GetAllPropositionsByCustomerId(int id)
        //{
        //    return _context.Propositions.Include(prop => prop.CustomerId).Include(prop => prop.EmployeeId).ToList();
        //}
    }
}
