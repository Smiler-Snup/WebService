using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.DataAccessLayer.Implementations;
using WebService.DataAccessLayer.Interfaces;
using WebService.Services.Interfaces;
using WebService.ViewModel;
using WebService.Model;

namespace WebService.Services.Implementations
{
    public class ServiceOfEmployee : IServiceOfEmployee
    {
        private readonly DatabaseManager DatabaseManager;
        public ServiceOfEmployee(DatabaseManager databaseManager)
        {
            DatabaseManager = databaseManager;
        }
        public int? AddEmployee(EmployeeViewModel employeeViewModel)
        {
            var Company = DatabaseManager.ImplementationAccessCompany.FindByName(employeeViewModel.CompanyName);
            if (Company == null)
                return null;

            var Department = DatabaseManager.ImplementationAccessDepartment.FindByName(employeeViewModel.DepartmentName);
            if (Department == null)
                return null;

            var Employee = new Employee
            {
                Name = employeeViewModel.Name,
                Surname = employeeViewModel.Surname,
                Phone = employeeViewModel.Phone,
                CompanyId = Company.Id,
                DepartmentID = Department.Id
            };

            var Passport = new Passport
            {
                Number = employeeViewModel.Number,
                Type = employeeViewModel.Type
            };

            var IDEmployee = DatabaseManager.ImplementationAccessEmployee.AddEmployee(Employee, Passport);

            return IDEmployee;

        }
    }
}
