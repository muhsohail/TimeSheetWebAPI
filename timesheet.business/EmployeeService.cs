using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using timesheet.data;
using timesheet.model;

namespace timesheet.business
{
    public class EmployeeService
    {
        public TimesheetDb db { get; }
        public EmployeeService(TimesheetDb dbContext)
        {
            this.db = dbContext;
        }

        public List<Employee> GetEmployees()
        {
            return this.db.Employees
                .Include(x =>x.TimeSheetEntries).ToList();
        }
    }
}
