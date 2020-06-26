﻿using EmployeeManagement.Models;
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

        [Parameter]
        public string EmployeeId { get; set; }

        public Employee Employee { get; set; } = new Employee();

        protected async override Task OnInitializedAsync()
        {
            Employee = await EmployeeService.GetEmployee(int.Parse(EmployeeId));
        }

    }
}
