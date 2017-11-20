using BLL.BusinessObjects;
using DAL.Entities;

namespace BLL.Converters
{
    public class CustomerConverter
    {
        public Customer Convert(CustomerBO cust)
        {
            if (cust == null) { return null; }
            {
                return new Customer()
                {
                    Id = cust.Id

                };
            }
        }

        public CustomerBO Convert(Customer cust)
        {
            if (cust == null) { return null; }
            return new CustomerBO()
            {
                Id = cust.Id
            };
        }




    }
}
