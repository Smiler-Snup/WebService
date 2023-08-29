using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Services.Interfaces;
using WebService.ViewModel;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IServiceOfEmployee ServiceOfEmployee;
        public EmployeeController(IServiceOfEmployee serviceOfEmployee)
        {
            ServiceOfEmployee = serviceOfEmployee;
        }


        [HttpPost]
        public async Task<ActionResult> Add([FromBody] EmployeeViewModel employeeViewModel)
        {
            return Ok("Hello");
        }
    }
}
