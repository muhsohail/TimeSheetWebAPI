using System;
using System.Collections.Generic;
using System.Text;
using timesheet.model;

namespace timesheet.business.Interface
{
    public interface ITaskService
    {
        List<Task> GetAll();
    }
}
