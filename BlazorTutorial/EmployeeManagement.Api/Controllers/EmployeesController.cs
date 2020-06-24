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

        public async Task<ActionResult<IEnumerable<Employee>>> Search(string name, Gender? gender)
        {
            return null;
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
                    return NotFound();
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
                    "Error adding data to the database");
            }
        }

        [HttpPut("{employeeId:int}")]
        public async Task<ActionResult<Employee>> UpdateEmployee(int employeeId, Employee employee)
        {
            try
            {
                if (employeeId != employee.EmployeeId)
                {
                    return BadRequest("Employee ID mismatch");
                }
                var result = await employeeRepsitory.UpdateEmployee(employee);
                if (result != null)
                {
                    return result;
                }
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
            
        }

        [HttpDelete("{employeeId:int}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int employeeId)
        {
            try
            {
                var employeeToDelete = await employeeRepsitory.GetEmployee(employeeId);
                if (employeeToDelete != null)
                {
                    return await employeeRepsitory.DeleteEmployee(employeeId);
                }
                return NotFound($"There is no employee with id {employeeId}");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                    "Error deleting data");
            }

        }
    }
}
