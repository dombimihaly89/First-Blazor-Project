using EmployeeManagement.Models;
using EmployeeManagement.Web.Services;
using Microsoft.AspNetCore.Components;
using Reusable.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Pages
{
    public class DisplayEmployeeBase : ComponentBase
    {
        [Inject]
        protected IEmployeeService EmployeeService { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Parameter]
        public Employee Employee { get; set; }
        [Parameter]
        public bool ShowFooter { get; set; }

        [Parameter]
        public EventCallback<bool> OnEmployeeSelection { get; set; }
        [Parameter]
        public EventCallback<int> OnEmployeeDeletion { get; set; }
        protected ConfirmBase DeleteConfirmation { get; set; }

        protected async Task CheckBoxChanged(ChangeEventArgs e)
        {
            await OnEmployeeSelection.InvokeAsync((bool)e.Value);
        }

        protected void Handle_Click_Delete()
        {
            DeleteConfirmation.Show();
            DeleteConfirmation.ConfirmationTitle = "Confirm Delete";
            DeleteConfirmation.ConfirmationMessage = "Are you sure you want to delete \"" + Employee.FirstName + "\"?";
        }

        protected async Task ConfirmDelete_Click(bool deleteConfirmed)
        {
            if (deleteConfirmed)
            {
                await EmployeeService.DeleteEmployee(Employee.EmployeeId);
                await OnEmployeeDeletion.InvokeAsync(Employee.EmployeeId);
            }
        }
    }
}
