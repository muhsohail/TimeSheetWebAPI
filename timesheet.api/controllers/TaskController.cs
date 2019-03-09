using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using timesheet.business.Interface;
using timesheet.model;

namespace timesheet.api.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public ActionResult<List<Task>> GetAll()
        {
            return _taskService.GetAll();
        }
    }
}