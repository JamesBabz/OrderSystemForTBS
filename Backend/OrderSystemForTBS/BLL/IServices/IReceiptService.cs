using BLL.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.IServices
{
    public interface IReceiptService
    {
        //C
        ReceiptBO Create(ReceiptBO bo);
        //R
        List<ReceiptBO> GetAllById(int customerId);
        List<ReceiptBO> GetNotificationList(int employeeId, DateTime lastLogin);
        ReceiptBO Get(int Id);
        //U
        ReceiptBO Update(ReceiptBO bo);
        //D
        ReceiptBO Delete(int Id);
    }
}
