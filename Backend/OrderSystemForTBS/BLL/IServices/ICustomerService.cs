using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    using BLL.BusinessObjects;

    public interface ICustomerService 
    {
        //C
        CustomerBO Create(CustomerBO cust);
        //R
        List<CustomerBO> GetAll();

        CustomerBO Get(int id);

        List<CustomerBO> GetAllBySearchQuery(string query);

        //U
        CustomerBO Update(CustomerBO cust);
        //D
        CustomerBO Delete(int id);
    }
}
