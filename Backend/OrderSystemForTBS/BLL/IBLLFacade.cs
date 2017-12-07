using BLL.Services;

namespace BLL
{
    public interface IBLLFacade
    {
        CustomerService CustomerService { get; }
        PropositionService PropositionService { get; }
        EmployeeService EmployeeService { get; }
        EquipmentService EquipmentService { get; }
        VisitService VisitService { get; }
        DawaService DawaService { get; }
    }
}
