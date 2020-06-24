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
        public async Task<ActionResult> GetEmployees()
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
        public async Task<ActionResult> GetEmployee(int employeeId)
        {
            try
            {
                var result = Ok(await employeeRepsitory.GetEmployee(employeeId));
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
    }
}
