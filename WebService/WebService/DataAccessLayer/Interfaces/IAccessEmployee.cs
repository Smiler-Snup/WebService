using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Model;

namespace WebService.DataAccessLayer.Interfaces
{
    public interface IAccessEmployee
    {
        int? AddEmployee(Employee employee,Passport passport);
        bool DeleteEmployee(int Id);
        IEnumerable<Employee> GetEmployeesByCompany(string companyName);
        IEnumerable<Employee> GetEmployeesByDepartment(string departmentName);
        bool UpdateEmployee(int employeeId, Employee employee);
    }
}
