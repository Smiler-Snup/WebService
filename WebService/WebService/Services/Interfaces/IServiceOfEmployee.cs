using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Model;
using WebService.OperationHandling;
using WebService.ViewModel;

namespace WebService.Services.Interfaces
{
    public interface IServiceOfEmployee
    {
        public ResultOperation<int?, string> AddEmployee(EmployeeViewModel employeeViewModel);
        public ResultOperation<int?, string> DeleteEmployee(int Id);
        public ResultOperation<IEnumerable<EmployeeViewModel>, string> GetEmployeesByCompany(string nameCompay);
        public ResultOperation<IEnumerable<EmployeeViewModel>, string> GetEmployeesByDepartment(string nameDepartment);
        public ResultOperation<EmployeeViewModel, string> UpdateEmployye(EmployeeViewModel employeeViewModel);
    }
}
