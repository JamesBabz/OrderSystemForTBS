using BLL.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.BusinessObjects;
using DAL;
using BLL.Converters;
using DAL.Entities;

namespace BLL.Services
{
    public class ReceiptService : IReceiptService
    {

        private IDALFacade _facade;
        private ReceiptConverter _recieptConv;
        private Receipt _newReceipt;
        private DateTime i;
        private Employee userFromDb;


        public ReceiptService(IDALFacade facade)
        {
            _facade = facade;
            _recieptConv = new ReceiptConverter();

        }

        public ReceiptBO Create(ReceiptBO bo)
        {
            using (var uow = _facade.UnitOfWork)
            {
                _newReceipt = uow.ReceiptRepository.Create(_recieptConv.Convert(bo));
                uow.Complete();
                return _recieptConv.Convert(_newReceipt);
            }
        }


        public ReceiptBO Get(int Id)
        {
            using (var uow = _facade.UnitOfWork)
            {
                _newReceipt = uow.ReceiptRepository.Get(Id);
                uow.Complete();
                return _recieptConv.Convert(_newReceipt);
            }
        }

        public ReceiptBO Update(ReceiptBO bo)
        {
            using (var uow = _facade.UnitOfWork)
            {
                // gets receipt from DB that matches the id
                var receiptFromDB = uow.ReceiptRepository.Get(bo.Id);
                receiptFromDB.Title = bo.Title;
                receiptFromDB.Description = bo.Description;
                receiptFromDB.FileId = bo.FileId;
                uow.Complete();
                return _recieptConv.Convert(receiptFromDB);
            }
        }

        public ReceiptBO Delete(int Id)
        {
            using (var uow = _facade.UnitOfWork)
            {
                _newReceipt = uow.ReceiptRepository.Get(Id);
                uow.ReceiptRepository.Delete(_newReceipt.Id);
                uow.Complete();
                return _recieptConv.Convert(_newReceipt);
            }
        }

        public List<ReceiptBO> GetAllById(int customerId)
        {
            using (var uow = _facade.UnitOfWork)
            {
                List<ReceiptBO> returnList = new List<ReceiptBO>();
                var fullList = uow.ReceiptRepository.GetAll(customerId);
                foreach (var receipt in fullList)
                {
                    returnList.Add(_recieptConv.Convert(receipt));
                }
                return returnList;
            }

        }

        public List<ReceiptBO> GetNotificationList(int employeeId, DateTime lastLogin)
        {
            using (var uow = _facade.UnitOfWork)
            {

                userFromDb = uow.EmployeeRepository.Get(employeeId);

                DateTime lastLoginTemp = userFromDb.LastLogin.Date;
                List<ReceiptBO> returnList = new List<ReceiptBO>();
                List<DateTime> dateList = new List<DateTime>();
                var fullList = uow.ReceiptRepository.GetAll(employeeId);

                for (i = DateTime.Today.Date; i >= lastLoginTemp; lastLoginTemp = lastLoginTemp.AddDays(+1))
                {

                    dateList.Add(lastLoginTemp);
                    Console.WriteLine(dateList.ToString());
                }

                foreach (var receipt in fullList)
                {
                    if (dateList.Contains(receipt.CreationDate = receipt.CreationDate.Date.AddYears(+1)))
                    {
                        returnList.Add(_recieptConv.Convert(receipt));
                    }

                }

                return returnList;
            }
        }
    }
}
