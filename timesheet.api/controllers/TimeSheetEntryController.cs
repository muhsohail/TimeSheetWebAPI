using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using timesheet.api.ViewModel;
using timesheet.business.Interface;
using timesheet.model;

namespace timesheet.api.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSheetEntryController : ControllerBase
    {
        private readonly ITimeSheetEntryService _timeSheetEntryService;

        public TimeSheetEntryController(ITimeSheetEntryService timeSheetEntryService)
        {
            _timeSheetEntryService = timeSheetEntryService;
        }

        [HttpGet]
        public ActionResult<List<TimeSheetEntry>> GetTimeSheetEntries()
        {
            return _timeSheetEntryService.GetAll();
        }

        [HttpPost]
        [ProducesResponseType(typeof(TimeSheetEntryViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddTimeSheetEntry([FromBody] List<TimeSheetEntryViewModel> viewModel)
        {
            try
            {
                List<TimeSheetEntry> ItemsToBeAdded = new List<TimeSheetEntry>();
                List<TimeSheetEntry> ItemsToBeUpdated = new List<TimeSheetEntry>();

                foreach (TimeSheetEntryViewModel item in viewModel)
                {
                    foreach (BreakdownByTask subItem in item.BreakdownByTask)
                    {
                        TimeSheetEntry timeSheetEntry = new TimeSheetEntry();
                        timeSheetEntry.TaskId = item.TaskId;
                        timeSheetEntry.WeekNo = item.WeekNo;
                        timeSheetEntry.EmployeeId = item.EmployeeId;

                        timeSheetEntry.HoursWorked = subItem.HoursWorked;
                        timeSheetEntry.DayOfWeek = (DayOfWeek)subItem.DayNo;
                        timeSheetEntry.Id = subItem.Id;

                        if (timeSheetEntry.Id != 0)
                            _timeSheetEntryService.UpdateUpdateTimeSheetEntry(timeSheetEntry);
                        else
                            ItemsToBeAdded.Add(timeSheetEntry);
                    }
                }

                if (ItemsToBeAdded.Count > 0)
                    _timeSheetEntryService.AddTimeSheetEntry(ItemsToBeAdded);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction(nameof(AddTimeSheetEntry), null);
        }

        // GET api/city
        [HttpGet("GetAllByEmployeeId/{EmployeeId}/{WeekNo}")]
        [ProducesResponseType(typeof(TimeSheetEntryViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<TimeSheetEntryViewModel>> GetAllByEmployeeId(int employeeId, int WeekNo)
        {
            List<TimeSheetEntryViewModel> model = new List<TimeSheetEntryViewModel>();
            List<TimeSheetEntry> timeSheetEntries = _timeSheetEntryService.GetTimeSheetEnteries(employeeId, WeekNo);

            model = timeSheetEntries
                .GroupBy(x => x.TaskId)
                .Select(x => new TimeSheetEntryViewModel
                {
                    TaskId = x.Key,
                    WeekNo = x.Select(y => y.WeekNo).FirstOrDefault(),
                    EmployeeId = x.Select(y => y.EmployeeId).FirstOrDefault(),
                    BreakdownByTask = x.OrderBy(y => y.DayOfWeek)
                    .GroupBy(s => s.DayOfWeek)
                    .Select(r => new BreakdownByTask
                    {
                        DayNo = (int)r.Key,
                        HoursWorked = r.Select(t => t.HoursWorked).FirstOrDefault(),
                        Id = r.Select(t => t.Id).FirstOrDefault(),                        
                    }).ToList()
                }).ToList();

            if (model == null)
                return NotFound();

            return model;
        }
    }
}