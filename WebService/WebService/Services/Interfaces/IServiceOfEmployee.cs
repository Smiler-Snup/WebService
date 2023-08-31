using System.Collections.Generic;
using WebService.OperationHandling;
using WebService.ViewModel;

namespace WebService.Services.Interfaces
{
    public interface IServiceOfEmployee
    {
        public ResultOperation<int?, string> AddEmployee(EmployeeAddViewModel employeeViewModel);
        public ResultOperation<int?, string> DeleteEmployee(int Id);
        public ResultOperation<IEnumerable<EmployeeOutputViewModel>, string> GetEmployeesByCompany(string nameCompay);
        public ResultOperation<IEnumerable<EmployeeOutputViewModel>, string> GetEmployeesByDepartment(string nameDepartment);
        public ResultOperation<EmployeeUpdateViewModel, string> UpdateEmployye(int Id, EmployeeUpdateViewModel employeeViewModel);
    }
}
