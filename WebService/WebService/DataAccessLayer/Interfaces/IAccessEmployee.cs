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
        void DeleteEmployee(int employeeId);
        IEnumerable<Employee> GetEmployeesByCompany(string companyName);
        IEnumerable<Employee> GetEmployeesByDepartment(string departmentName);
        void UpdateEmployee(int employeeId, Employee employee);
    }
}
