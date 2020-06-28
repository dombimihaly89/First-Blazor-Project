using AutoMapper;
using EmployeeManagement.Models;
using EmployeeManagement.Web.Models;
using EmployeeManagement.Web.Services;
using Microsoft.AspNetCore.Components;
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

        public Employee Employee { get; set; } = new Employee();
        public EditEmployeeModel EditEmployeeModel { get; set; } = new EditEmployeeModel();
        public List<Department> Departments { get; set; } = new List<Department>();

        protected async override Task OnInitializedAsync()
        {
            Employee = await EmployeeService.GetEmployee(int.Parse(EmployeeId));
            Departments = (await DepartmentService.GetDepartments()).ToList();

            Mapper.Map(Employee, EditEmployeeModel);
        }

        protected async void HandleValidSubmit()
        {
            Mapper.Map(EditEmployeeModel, Employee);
            var result = await EmployeeService.UpdateEmployee(Employee.EmployeeId, Employee);
            if (result != null)
            {
                NavigationManager.NavigateTo("/");
            }

        }
    }
}
