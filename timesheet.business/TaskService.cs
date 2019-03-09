using System.Collections.Generic;
using System.Linq;
using timesheet.business.Interface;
using timesheet.data;
using timesheet.model;

namespace timesheet.business
{
    public class TaskService : ITaskService
    {
        private readonly TimesheetDb _context;

        public TaskService(TimesheetDb context)
        {
            _context = context;

        }
        public List<Task> GetAll()
        {
            return _context.Tasks.ToList();
        }
    }
}
