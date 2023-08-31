using WebService.OperationHandling;
using WebService.ViewModel.Base;

namespace WebService.Services.Validation.Interfaces
{
    public interface IValidationEmployeeData
    {
        public ResultOperation<EmployeeBaseViewModel, string> IsValid(EmployeeBaseViewModel employeeViewModel);
    }
}
