using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Pages
{
    public class DataBindingDemoBase : ComponentBase
    {
        protected string Name { get; set; } = "Tom";
        protected string Gender { get; set; } = "Male";
        protected string Colour { get; set; } = "background-color:red";

        //protected void Name_Change(ChangeEventArgs args)
        //{
        //    Name = args.Value.ToString();
        //}
    }
}
