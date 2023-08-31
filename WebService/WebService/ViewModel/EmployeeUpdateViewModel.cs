using WebService.ViewModel.Base;

namespace WebService.ViewModel
{
    public class EmployeeUpdateViewModel: EmployeeBaseViewModel
    {
        public int? IdCompany { get; set; }
        public int? IdDepartment { get; set; }
    }
}
