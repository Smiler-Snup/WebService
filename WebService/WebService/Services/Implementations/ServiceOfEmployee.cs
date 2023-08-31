using System.Collections.Generic;
using WebService.DataAccessLayer.Implementations;
using WebService.Services.Interfaces;
using WebService.ViewModel;
using WebService.Model;
using WebService.OperationHandling;
using WebService.DataAccessLayer.DTO;

namespace WebService.Services.Implementations
{
    public class ServiceOfEmployee : IServiceOfEmployee
    {
        private readonly DatabaseManager DatabaseManager;
        public ServiceOfEmployee(DatabaseManager databaseManager)
        {
            DatabaseManager = databaseManager;
        }
        /// <summary>
        /// Добавляет сотрудника в систему
        /// Внешние ключи для компании и отдела определяются по нахождению их по имени
        /// </summary>
        /// <param name="employeeViewModel"></param>
        /// <returns></returns>
        public ResultOperation<int?, string> AddEmployee(EmployeeAddViewModel employeeViewModel)
        {
            var Company = DatabaseManager.ImplementationAccessCompany.FindByName(employeeViewModel.CompanyName);
            if (Company == null)
                return ResultOperation<int?, string>.Error("Компания не была найдена по наименованию");

            var Department = DatabaseManager.ImplementationAccessDepartment.FindByName(employeeViewModel.DepartmentName);
            if (Department == null)
                return ResultOperation<int?, string>.Error("Отдел не был найден по наименованию");

            var Employee = new EmployeeDTO
            {
                Name = employeeViewModel.Name,
                Surname = employeeViewModel.Surname,
                Phone = employeeViewModel.Phone,
                CompanyId = Company.Id,
                DepartmentId = Department.Id,
                Number = employeeViewModel.Number,
                Type = employeeViewModel.Type
            };

            var IDEmployee = DatabaseManager.ImplementationAccessEmployee.AddEmployee(Employee);

            if(IDEmployee==null)
                return ResultOperation<int?, string>.Error("Новый сотрудник не добавлен в систему, проверьте введенные данные");

            return ResultOperation<int?, string>.Success(IDEmployee);

        }

        /// <summary>
        /// Удяляет сотрудника из базы данных по уникальному идентификатору Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResultOperation<int?, string> DeleteEmployee(int Id)
        {
            if (!DatabaseManager.ImplementationAccessEmployee.DeleteEmployee(Id))
                return ResultOperation<int?,string>.Error($"Сотрудника с ID:{Id} система не смогла удалить");

            return ResultOperation<int?, string>.Success(Id);
        }

        /// <summary>
        /// Возвращает коллекцию с информацией о сотрудниках, принадлежащих одной компании
        /// Компания определяется по наименованию
        /// </summary>
        /// <param name="nameCompay"></param>
        /// <returns></returns>
        public ResultOperation<IEnumerable<EmployeeOutputViewModel>, string> GetEmployeesByCompany(string nameCompay)
        {
            var company = DatabaseManager.ImplementationAccessCompany.FindByName(nameCompay);
            if (company == null)
                return ResultOperation<IEnumerable<EmployeeOutputViewModel>, string>.Error("Компания не была найдена по наименованию");

            var employees = DatabaseManager.ImplementationAccessEmployee.GetEmployeesByCompany(company);

            var employeeViewModels = new List<EmployeeOutputViewModel>();

            foreach(var employee in employees)
            {
                var passport = DatabaseManager.ImplementationAccessPassport.FindById(employee.Id);

                var department = DatabaseManager.ImplementationAccessDepartment.FindById(employee.DepartmentId);

                employeeViewModels.Add(new EmployeeOutputViewModel
                {
                    Name=employee.Name,
                    Surname=employee.Surname,
                    Phone = employee.Phone,
                    DepartmentName = department?.Name,
                    DepartmentPhone = department?.Phone,
                    CompanyName = company.Name,
                    Type = passport?.Type,
                    Number = passport?.Number
                });
            }

            return ResultOperation<IEnumerable<EmployeeOutputViewModel>, string>.Success(employeeViewModels);
        }

        /// <summary>
        /// Возвращает коллекцию с информацией о сотрудниках, принадлежащих одному типу отдела
        /// Отдел определяется по наименованию
        /// </summary>
        /// <param name="nameDepartment"></param>
        /// <returns></returns>
        public ResultOperation<IEnumerable<EmployeeOutputViewModel>, string> GetEmployeesByDepartment(string nameDepartment)
        {
            var department = DatabaseManager.ImplementationAccessDepartment.FindByName(nameDepartment);
            if (department == null)
                return ResultOperation<IEnumerable<EmployeeOutputViewModel>, string>.Error("Отдел не был найден по наименованию");

            var employees = DatabaseManager.ImplementationAccessEmployee.GetEmployeesByDepartment(department);

            var employeeViewModels = new List<EmployeeOutputViewModel>();

            foreach (var employee in employees)
            {
                var passport = DatabaseManager.ImplementationAccessPassport.FindById(employee.Id);

                var company = DatabaseManager.ImplementationAccessCompany.FindById(employee.CompanyId);

                employeeViewModels.Add(new EmployeeOutputViewModel
                {
                    Name = employee.Name,
                    Surname = employee.Surname,
                    Phone = employee.Phone,
                    DepartmentName = department.Name,
                    DepartmentPhone = department.Phone,
                    CompanyName = company?.Name,
                    Type = passport?.Type,
                    Number = passport?.Number
                });
            }
            return ResultOperation<IEnumerable<EmployeeOutputViewModel>, string>.Success(employeeViewModels);
        }

        /// <summary>
        /// Обновляются поля сотрудника
        /// Сотрудник обновляется по Id
        /// Внешние ключи по отделу и компании определяются по наименованию
        /// Если наименования компании или отдела были переданы, но не были найдены, то операция возвращает ошибку
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="employeeViewModel"></param>
        /// <returns></returns>
        public ResultOperation<EmployeeUpdateViewModel, string> UpdateEmployye(int Id, EmployeeUpdateViewModel employeeViewModel)
        {
            var Company = DatabaseManager.ImplementationAccessCompany.FindByName(employeeViewModel.CompanyName);
            if(employeeViewModel.CompanyName!=null && Company==null)
                return ResultOperation<EmployeeUpdateViewModel, string>.Error("Невозможно обновить компанию сотрудника, так как указанная компания не найдена в базе данных.");

            var Department = DatabaseManager.ImplementationAccessDepartment.FindByName(employeeViewModel.DepartmentName);
            if (employeeViewModel.DepartmentName != null && Department == null)
                return ResultOperation<EmployeeUpdateViewModel, string>.Error("Невозможно обновить отдел сотрудника, так как указанный отдел не найден в базе данных.");

            var Employee = new EmployeeDTO
            {
                Id = Id,
                Name = employeeViewModel?.Name,
                Surname = employeeViewModel?.Surname,
                Phone = employeeViewModel?.Phone,
                CompanyId = Company?.Id,
                DepartmentId = Department?.Id,
                Number = employeeViewModel?.Number,
                Type = employeeViewModel?.Type
            };

            if(!DatabaseManager.ImplementationAccessEmployee.UpdateEmployee(Employee))
                return ResultOperation<EmployeeUpdateViewModel, string>.Error("Невозможно обновить данные сотрудника, проверьте введенные данные");

            return ResultOperation<EmployeeUpdateViewModel, string>.Success(employeeViewModel);
        }
    }
}
