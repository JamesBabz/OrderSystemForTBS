using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Context;
using DAL.Entities;
using DAL.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private OrderSystemContext _context;

        public EquipmentRepository(OrderSystemContext context)
        {
            _context = context;
        }

        public Equipment Create(Equipment ent)
        {
            _context.Equipments.Add(ent);
            return ent;
        }

        public Equipment Get(int Id)
        {
            return _context.Equipments.FirstOrDefault(equip => equip.Id == Id);
        }

        public Equipment Delete(int Id)
        {
            var equipment = Get(Id);
            _context.Equipments.Remove(equipment);
            return equipment;
        }
        
        public IEnumerable<Equipment> GetAllById(int id)
        {
            return _context.Equipments.Include(equip => equip.Customer).Where(x => x.CustomerId == id).ToList();
        }
    }
}
