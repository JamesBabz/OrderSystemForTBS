using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.BusinessObjects;
using BLL.Converters;
using DAL;
using DAL.Entities;

namespace BLL.Services
{
    public class EquipmentService : IService<EquipmentBO>
    {
        private IDALFacade facade;
        private EquipmentConverter equipmentConverter = new EquipmentConverter();
        private Equipment newEquipment;

        public EquipmentService(IDALFacade facade)
        {
            this.facade = facade;
        }


        public EquipmentBO Create(EquipmentBO equip)
        {
            using (var uow = facade.UnitOfWork)
            {
                newEquipment = uow.EquipmentRepository.Create(equipmentConverter.Convert(equip));
                uow.Complete();
                return equipmentConverter.Convert(newEquipment);
            }
        }

        public List<EquipmentBO> GetAll()
        {
            using (var uow = facade.UnitOfWork)
            {
                return uow.EquipmentRepository.GetAll().Select(equipmentConverter.Convert).ToList();
            }
        }

        public List<EquipmentBO> GetAllById(int customerId)
        {
            using (var uow = facade.UnitOfWork)
            {
                List<EquipmentBO> returnList = new List<EquipmentBO>();
                var fullList = uow.EquipmentRepository.GetAll(customerId);
                foreach (var equip in fullList)
                {
                    returnList.Add(equipmentConverter.Convert(equip));
                }
                Console.Write(returnList.ToString());

                return returnList;
            }
        }

        public EquipmentBO Get(int Id)
        {
            using (var uow = facade.UnitOfWork)
            {
                newEquipment = uow.EquipmentRepository.Get(Id);
                uow.Complete();
                return equipmentConverter.Convert(newEquipment);
            }
        }

        public EquipmentBO Update(EquipmentBO bo)
        {
            throw new NotImplementedException();
        }

        public EquipmentBO Delete(int Id)
        {
            using (var uow = facade.UnitOfWork)
            {
                newEquipment = uow.EquipmentRepository.Delete(Id);
                uow.Complete();
                return equipmentConverter.Convert(newEquipment);
            }
        }
    }
}
