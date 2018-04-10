using BLL.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using BLL.BusinessObjects;
using BLL.Converters;
using DAL;
using DAL.Entities;

namespace BLL.Services
{
    public class SalesmanListService : ISalesmanListService
    {

        private IDALFacade _facade;
        private SalesmanListConverter _salesmanListConverter;
        private SalesmanList _newSalesmanList;

        public SalesmanListService(IDALFacade facade)
        {
            _salesmanListConverter = new SalesmanListConverter();
            _facade = facade;
        }

        public SalesmanListBO Create(SalesmanListBO BO)
        {
            using (var uow = _facade.UnitOfWork)
            {
                _newSalesmanList = uow.SalesmanListRepository.Create(_salesmanListConverter.Convert(BO));
                uow.Complete();
                return _salesmanListConverter.Convert(_newSalesmanList);
            }
        }

        public SalesmanListBO Delete(int Id)
        {
            using (var uow = _facade.UnitOfWork)
            {
                _newSalesmanList = uow.SalesmanListRepository.Delete(Id);
                uow.Complete();
                return _salesmanListConverter.Convert(_newSalesmanList);
            }
        }

        public SalesmanListBO Get(int Id)
        {
            throw new NotImplementedException();
        }

        public List<SalesmanListBO> GetAllById(int EmployeeId)
        {
            using (var uow = _facade.UnitOfWork)
            {
                List<SalesmanListBO> returnList = new List<SalesmanListBO>();
                var fullList = uow.SalesmanListRepository.GetAllById(EmployeeId);
                foreach (var x in fullList)
                {
                    returnList.Add(_salesmanListConverter.Convert(x));
                }

                return returnList;
            }
        }
    }
}
