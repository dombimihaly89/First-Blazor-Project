using EmployeeManagement.Api.Models;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepsitory employeeRepsitory;

        public EmployeesController(IEmployeeRepsitory employeeRepsitory)
        {
            this.employeeRepsitory = employeeRepsitory;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            try
            {
                return Ok(await employeeRepsitory.GetEmployees());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{employeeId:int}")]
        public async Task<ActionResult<Employee>> GetEmployee(int employeeId)
        {
            try
            {
                var result = await employeeRepsitory.GetEmployee(employeeId);
                if (result == null)
                {
                    Console.WriteLine("here i am");
                    NotFound();
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            try
            {
                if(employee == null)
                {
                    return BadRequest();
                }
                var employeeWithSameEmail = await employeeRepsitory.GetEmployeeByEmail(employee.Email);
                if (employeeWithSameEmail != null)
                {
                    ModelState.AddModelError("email", "This email is already in use by an other employee");
                    return BadRequest(ModelState);
                }
                var createdEmployee = await employeeRepsitory.AddEmployee(employee);

                return CreatedAtAction(nameof(GetEmployee), new { employeeId = createdEmployee.EmployeeId }, createdEmployee);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
    }
}
