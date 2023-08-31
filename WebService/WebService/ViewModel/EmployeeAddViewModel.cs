using System.ComponentModel.DataAnnotations;
using WebService.ViewModel.Base;

namespace WebService.ViewModel
{
    public class EmployeeAddViewModel:EmployeeBaseViewModel
    {
        [Required]
        public override string Name { get; set; }
        [Required]
        public override string Surname { get; set; }
        [Required]
        public override string Phone { get; set; }
        [Required]
        public override string CompanyName { get; set; }
        [Required]
        public override string DepartmentName { get; set; }
        [Required]
        public override string Type { get; set; }
        [Required]
        public override string Number { get; set; }
    }
}
