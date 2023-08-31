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

        /// <summary>
        /// Добавляет нового сотрудника в систему
        /// </summary>
        /// <param name="employeeViewModel"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Удаляет сотрудника по уникальному идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] int id)
        {
            var resultOperation = ServiceOfEmployee.DeleteEmployee(id);

            if (!resultOperation.IsSuccess)
                return BadRequest(resultOperation.ErrorValue);

            return Ok("Удаление успешно было произведено");
        }

        /// <summary>
        /// Возвращает сотрудников, которые принадлежат одной компании
        /// </summary>
        /// <param name="nameCompany"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetByCompany")]
        public async Task<ActionResult> GetByCompany([FromQuery] int idCompany)
        {
            var resultOperation = ServiceOfEmployee.GetEmployeesByCompany(idCompany);

            if (!resultOperation.IsSuccess)
                return BadRequest(resultOperation.ErrorValue);

            return Ok(resultOperation.SuccessValue);
        }

        /// <summary>
        /// Возвращает сотрудников, которые принадлежат одному типу отделу
        /// </summary>
        /// <param name="nameDepartment"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetByDepartment")]
        public async Task<ActionResult> GetByDepartment([FromQuery] int idCompany, [FromQuery] int idDepartment)
        {
            var resultOperation = ServiceOfEmployee.GetEmployeesByDepartment(idCompany, idDepartment);

            if (!resultOperation.IsSuccess)
                return BadRequest(resultOperation.ErrorValue);

            return Ok(resultOperation.SuccessValue);
        }

        /// <summary>
        /// Обновляет поля сотрудника. Поиск сотрудника осуществляется по уникальному идентификатору
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="employeeViewModel"></param>
        /// <returns></returns>
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
