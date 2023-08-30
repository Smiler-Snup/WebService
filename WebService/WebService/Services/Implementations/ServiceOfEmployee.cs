using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.DataAccessLayer.Implementations;
using WebService.DataAccessLayer.Interfaces;
using WebService.Services.Interfaces;
using WebService.ViewModel;
using WebService.Model;
using WebService.OperationHandling;

namespace WebService.Services.Implementations
{
    public class ServiceOfEmployee : IServiceOfEmployee
    {
        private readonly DatabaseManager DatabaseManager;
        public ServiceOfEmployee(DatabaseManager databaseManager)
        {
            DatabaseManager = databaseManager;
        }
        public ResultOperation<int?, string> AddEmployee(EmployeeViewModel employeeViewModel)
        {
            var Company = DatabaseManager.ImplementationAccessCompany.FindByName(employeeViewModel.CompanyName);
            if (Company == null)
                return ResultOperation<int?, string>.Error("Компания не была найдена по наименованию");

            var Department = DatabaseManager.ImplementationAccessDepartment.FindByName(employeeViewModel.DepartmentName);
            if (Department == null)
                return ResultOperation<int?, string>.Error("Отдел не был найден по наименованию");

            var Employee = new Employee
            {
                Name = employeeViewModel.Name,
                Surname = employeeViewModel.Surname,
                Phone = employeeViewModel.Phone,
                CompanyId = Company.Id,
                DepartmentId = Department.Id
            };

            var Passport = new Passport
            {
                Number = employeeViewModel.Number,
                Type = employeeViewModel.Type
            };

            var IDEmployee = DatabaseManager.ImplementationAccessEmployee.AddEmployee(Employee, Passport);

            if(IDEmployee==null)
                return ResultOperation<int?, string>.Error("Новый сотрудник не добавлен в систему, проверьте введенные данные");

            return ResultOperation<int?, string>.Success(IDEmployee);

        }

        public ResultOperation<int?, string> DeleteEmployee(int Id)
        {
            if (!DatabaseManager.ImplementationAccessEmployee.DeleteEmployee(Id))
                return ResultOperation<int?,string>.Error($"Сотрудника с ID:{Id} система не смогла удалить");

            return ResultOperation<int?, string>.Success(Id);
        }

        public ResultOperation<IEnumerable<EmployeeViewModel>, string> GetEmployeesByCompany(string nameCompay)
        {
            throw new NotImplementedException();
        }

        public ResultOperation<IEnumerable<EmployeeViewModel>, string> GetEmployeesByDepartment(string nameDepartment)
        {
            throw new NotImplementedException();
        }

        public ResultOperation<EmployeeViewModel, string> UpdateEmployye(EmployeeViewModel employeeViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
