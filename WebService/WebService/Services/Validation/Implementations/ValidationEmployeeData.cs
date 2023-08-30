using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Services.Validation.Interfaces;
using WebService.OperationHandling;
using WebService.ViewModel;
using System.Text.RegularExpressions;

namespace WebService.Services.Validation.Implementations
{
    public class ValidationEmployeeData : IValidationEmployeeData
    {
        public ResultOperation<EmployeeViewModel,string> IsValid(EmployeeViewModel employeeViewModel)
        {
            if(employeeViewModel==null)
            {
                return ResultOperation<EmployeeViewModel, string>.Error("Данные некорректны");
            }

            if(!IsValidPhoneFormat(employeeViewModel.Phone))
            {
                return ResultOperation<EmployeeViewModel, string>.Error("Номер телефона может содержать только символы: цифры,+,-");
            }

            return ResultOperation<EmployeeViewModel, string>.Success(employeeViewModel);

        }

        private bool IsValidPhoneFormat(string phone)
        {
            string phonePattern = @"^\+?[\d\-]+$";

            return Regex.IsMatch(phone, phonePattern);
        }
    }
}
