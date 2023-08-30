using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Services.Interfaces;
using WebService.Services.Validation.Interfaces;
using WebService.ViewModel;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IServiceOfEmployee ServiceOfEmployee;
        private readonly IValidationEmployeeData ValidationEmployeeData;
        public EmployeeController(IServiceOfEmployee serviceOfEmployee, IValidationEmployeeData validationEmployeeData)
        {
            ServiceOfEmployee = serviceOfEmployee;
            ValidationEmployeeData = validationEmployeeData;
        }


        [HttpPost]
        public async Task<ActionResult> Add([FromBody] EmployeeViewModel employeeViewModel)
        {
            var resultValidation = ValidationEmployeeData.IsValid(employeeViewModel);

            if (!resultValidation.IsSuccess)
                return BadRequest(resultValidation.ErrorValue);

            var resultOperation = ServiceOfEmployee.AddEmployee(employeeViewModel);

            if(!resultOperation.IsSuccess)
                return BadRequest(resultOperation.ErrorValue);

            return Ok(resultOperation.SuccessValue);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] int Id)
        {
            var resultOperation = ServiceOfEmployee.DeleteEmployee(Id);

            if (!resultOperation.IsSuccess)
                return BadRequest(resultOperation.ErrorValue);

            return Ok("Удаление успешно было произведено");
        }
    }
}
