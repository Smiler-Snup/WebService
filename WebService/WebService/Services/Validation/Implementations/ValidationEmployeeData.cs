using WebService.Services.Validation.Interfaces;
using WebService.OperationHandling;
using System.Text.RegularExpressions;
using WebService.ViewModel.Base;

namespace WebService.Services.Validation.Implementations
{
    public class ValidationEmployeeData : IValidationEmployeeData
    {
        public ResultOperation<EmployeeBaseViewModel, string> IsValid(EmployeeBaseViewModel employeeViewModel)
        {
            if(employeeViewModel==null)
            {
                return ResultOperation<EmployeeBaseViewModel, string>.Error("Данные некорректны");
            }

            if(!IsValidPhoneFormat(employeeViewModel.Phone))
            {
                return ResultOperation<EmployeeBaseViewModel, string>.Error("Номер телефона может содержать только символы: цифры,+,-");
            }

            return ResultOperation<EmployeeBaseViewModel, string>.Success(employeeViewModel);

        }

        private bool IsValidPhoneFormat(string phone)
        {
            string phonePattern = @"^\+?[\d\-]+$";

            return Regex.IsMatch(phone, phonePattern);
        }
    }
}
