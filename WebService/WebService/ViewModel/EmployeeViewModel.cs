using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.ViewModel
{
    public class EmployeeViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string DepartmentName { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Number { get; set; }
    }
}
