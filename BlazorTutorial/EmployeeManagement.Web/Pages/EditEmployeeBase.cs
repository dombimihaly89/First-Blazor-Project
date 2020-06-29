using AutoMapper;
using EmployeeManagement.Models;
using EmployeeManagement.Web.Models;
using EmployeeManagement.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Pages
{
    public class EditEmployeeBase : ComponentBase
    {
        [Inject]
        protected IEmployeeService EmployeeService { get; set; }
        [Inject]
        protected IDepartmentService DepartmentService { get; set; }
        [Inject]
        protected IMapper Mapper { get; set; }
        [Inject] 
        protected NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string EmployeeId { get; set; }

        public string PageHeaderText { get; set; }
        public Employee Employee { get; set; } = new Employee();
        public EditEmployeeModel EditEmployeeModel { get; set; } = new EditEmployeeModel();
        public List<Department> Departments { get; set; } = new List<Department>();

        protected async override Task OnInitializedAsync()
        {
            int.TryParse(EmployeeId, out int employeeId);
            if (employeeId != 0)
            {
                Employee = await EmployeeService.GetEmployee(employeeId);
                PageHeaderText = "Edit Employee";
            } else
            {
                Employee.DepartmentId = 1;
                Employee.DateOfBirth = DateTime.Today;
                Employee.PhotoPath = "images/nophoto.jpg";
                PageHeaderText = "Create New Employee";
            }
            Departments = (await DepartmentService.GetDepartments()).ToList();
            Mapper.Map(Employee, EditEmployeeModel);
        }

        protected async void HandleValidSubmit()
        {
            Mapper.Map(EditEmployeeModel, Employee);
            if (Employee.EmployeeId != 0)
            {
                Employee = await EmployeeService.UpdateEmployee(Employee.EmployeeId, Employee);
            }
            else
            {
                Employee = await EmployeeService.CreateEmployee(Employee);
            }
            if (Employee != null)
            {
                NavigationManager.NavigateTo("/");
            }

        }

        protected async Task Delete_Click()
        {
            await EmployeeService.DeleteEmployee(Employee.EmployeeId);
            NavigationManager.NavigateTo("/");
        }
    }
}
