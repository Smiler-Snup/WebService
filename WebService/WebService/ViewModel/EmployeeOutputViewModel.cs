using WebService.ViewModel.Base;

namespace WebService.ViewModel
{
    /// <summary>
    /// Класс представляет данные, которые будут отображаться пользователю о сотруднике
    /// </summary>
    public class EmployeeOutputViewModel: EmployeeBaseViewModel
    {
        public string CompanyName { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentPhone { get; set; }
    }
}
