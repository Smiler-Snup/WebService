using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.OperationHandling;
using WebService.ViewModel;

namespace WebService.Services.Validation.Interfaces
{
    public interface IValidationEmployeeData
    {
        public ResultOperation<EmployeeViewModel, string> IsValid(EmployeeViewModel employeeViewModel);
    }
}
