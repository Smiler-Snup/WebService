using System.Collections.Generic;
using WebService.DataAccessLayer.DTO;
using WebService.Model;

namespace WebService.DataAccessLayer.Interfaces
{
    public interface IAccessEmployee
    {
        int? AddEmployee(EmployeeDTO employeeDTO);
        bool DeleteEmployee(int Id);
        IEnumerable<Employee> GetEmployeesByCompany(Company company);
        IEnumerable<Employee> GetEmployeesByDepartment(Department department);
        bool UpdateEmployee(EmployeeDTO employeeDTO);
    }
}
