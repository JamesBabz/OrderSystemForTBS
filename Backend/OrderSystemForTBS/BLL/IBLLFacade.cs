using BLL.Services;

namespace BLL
{
    public interface IBLLFacade
    {
        CustomerService CustomerService { get; }
        EmployeeService EmployeeService { get; }
    }
}
