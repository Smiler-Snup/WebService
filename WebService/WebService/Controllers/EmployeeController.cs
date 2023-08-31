using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult> Add([FromBody] EmployeeAddViewModel employeeViewModel)
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
        public async Task<ActionResult> Delete([FromBody] int id)
        {
            var resultOperation = ServiceOfEmployee.DeleteEmployee(id);

            if (!resultOperation.IsSuccess)
                return BadRequest(resultOperation.ErrorValue);

            return Ok("Удаление успешно было произведено");
        }

        [HttpGet]
        [Route("GetByCompany")]
        public async Task<ActionResult> GetByCompany([FromQuery] string nameCompany)
        {
            var resultOperation = ServiceOfEmployee.GetEmployeesByCompany(nameCompany);

            if (!resultOperation.IsSuccess)
                return BadRequest(resultOperation.ErrorValue);

            return Ok(resultOperation.SuccessValue);
        }

        [HttpGet]
        [Route("GetByDepartment")]
        public async Task<ActionResult> GetByDepartment([FromQuery] string nameDepartment)
        {
            var resultOperation = ServiceOfEmployee.GetEmployeesByDepartment(nameDepartment);

            if (!resultOperation.IsSuccess)
                return BadRequest(resultOperation.ErrorValue);

            return Ok(resultOperation.SuccessValue);
        }
        [HttpPut]
        public async Task<ActionResult> Update([FromQuery] int Id, [FromBody] EmployeeUpdateViewModel employeeViewModel)
        {

            var resultOperation = ServiceOfEmployee.UpdateEmployye(Id,employeeViewModel);

            if (!resultOperation.IsSuccess)
                return BadRequest(resultOperation.ErrorValue);

            return Ok(resultOperation.SuccessValue);
        }

    }
}
