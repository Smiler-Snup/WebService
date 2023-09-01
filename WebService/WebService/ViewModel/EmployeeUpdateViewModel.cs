using WebService.ViewModel.Base;

namespace WebService.ViewModel
{
    /// <summary>
    /// Класс представляет данные, которые необходимы для обновления сотрудника
    /// </summary>
    public class EmployeeUpdateViewModel: EmployeeBaseViewModel
    {
        public int? IdCompany { get; set; }
        public int? IdDepartment { get; set; }
    }
}
