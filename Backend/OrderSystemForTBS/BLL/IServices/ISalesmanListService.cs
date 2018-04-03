using BLL.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.IServices
{
    public interface ISalesmanListService
    {
        SalesmanListBO Create(SalesmanListBO BO);
        SalesmanListBO Get(int Id);
        List<SalesmanListBO> GetAllById(int EmployeeId);
        SalesmanListBO Delete(int Id);
    }
}
