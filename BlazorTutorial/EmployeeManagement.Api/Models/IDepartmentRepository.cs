﻿using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Api.Models
{
    interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> getDepartments();
        Task<Department> getDepartment(int departmentId);
    }
}
