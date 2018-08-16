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
        ReceiptBO Get(int Id);
        //U
        ReceiptBO Update(ReceiptBO bo);
        //D
        ReceiptBO Delete(int Id);
    }
}
