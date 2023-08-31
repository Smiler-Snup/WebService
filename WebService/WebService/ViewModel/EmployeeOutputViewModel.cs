using WebService.ViewModel.Base;

namespace WebService.ViewModel
{
    public class EmployeeOutputViewModel: EmployeeBaseViewModel
    {
        public string CompanyName { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentPhone { get; set; }
    }
}
