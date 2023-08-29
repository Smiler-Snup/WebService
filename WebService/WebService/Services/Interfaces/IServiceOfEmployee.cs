using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Model;
using WebService.ViewModel;

namespace WebService.Services.Interfaces
{
    public interface IServiceOfEmployee
    {
        public int? AddEmployee(EmployeeViewModel employeeViewModel);
    }
}
