using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Api.Models
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext appDbContext;

        public DepartmentRepository(AppDbContext appDbContext) 
        {
            this.appDbContext = appDbContext;
        }
        public async Task<Department> getDepartment(int departmentId)
        {
            return await appDbContext.Departments.FirstOrDefaultAsync(dept => dept.DepartmentId == departmentId);
        }

        public async Task<IEnumerable<Department>> getDepartments()
        {
            return await appDbContext.Departments.ToListAsync();
        }
    }
}
