using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using timesheet.api.ViewModel;
using timesheet.business;
using timesheet.model;

namespace timesheet.api.controllers
{
    [Route("api/v1/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService employeeService;
        public EmployeeController(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll(string text)
        {
            List<WeeklyEffortViewModel> model = new List<WeeklyEffortViewModel>();
            List<Employee> items = this.employeeService.GetEmployees().ToList();

            model = items.Select(x => new WeeklyEffortViewModel
            {
                Id= x.Id,
                Code = x.Code,
                Name = x.Name,
                TotalWeekEffort = x.TimeSheetEntries.Sum(y => y.HoursWorked),
                AverageWeekEffort = Math.Round(((double)(x.TimeSheetEntries.Sum(y => y.HoursWorked)) / 7), 2)
            }).ToList();

            
            return new ObjectResult(model);
        }

        [HttpGet("GetWeeklyEffort")]
        public IActionResult GetWeeklyEffort(int employeeId)
        {
            //List<TimeSheetEntry> WeeklyEffortDetails = this.employeeService.GetWeeklyEffort(employeeId);


            return null;
            //return new ObjectResult(items);
        }

    }
}