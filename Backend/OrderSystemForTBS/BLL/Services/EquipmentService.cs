using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.BusinessObjects;
using BLL.Converters;
using BLL.IServices;
using DAL;
using DAL.Entities;

namespace BLL.Services
{
    public class EquipmentService : IEquipmentService
    {
        private IDALFacade _facade;

        private EquipmentConverter _equipmentConverter;
        private Equipment _newEquipment;

        public EquipmentService(IDALFacade facade)
        {
            _facade = facade;
            _equipmentConverter = new EquipmentConverter();
        }


        public EquipmentBO Create(EquipmentBO equip)
        {
            using (var uow = _facade.UnitOfWork)
            {
                _newEquipment = uow.EquipmentRepository.Create(_equipmentConverter.Convert(equip));
                uow.Complete();
                return _equipmentConverter.Convert(_newEquipment);
            }
        }

        public List<EquipmentBO> GetAllById(int customerId)
        {
            using (var uow = _facade.UnitOfWork)
            {
                List<EquipmentBO> returnList = new List<EquipmentBO>();
                var fullList = uow.EquipmentRepository.GetAllById(customerId);
                foreach (var equip in fullList)
                {
                    returnList.Add(_equipmentConverter.Convert(equip));
                }

                return returnList;
            }
        }

        // TODO a smarter way to get a single equip 
        public EquipmentBO Get(int Id)
        {
            using (var uow = _facade.UnitOfWork)
            {
                _newEquipment = uow.EquipmentRepository.Get(Id);
                uow.Complete();
                return _equipmentConverter.Convert(_newEquipment);
            }
        }

        public EquipmentBO Delete(int Id)
        {
            using (var uow = _facade.UnitOfWork)
            {
                _newEquipment = uow.EquipmentRepository.Get(Id);
                uow.EquipmentRepository.Delete(_newEquipment.Id);
                uow.Complete();
                return _equipmentConverter.Convert(_newEquipment);
            }
        }
    }
}
