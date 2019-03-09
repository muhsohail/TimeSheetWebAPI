using System;
using System.Collections.Generic;
using System.Linq;
using timesheet.business.Interface;
using timesheet.data;
using timesheet.model;

namespace timesheet.business
{
    public class TimeSheetEntryService : ITimeSheetEntryService
    {
        private readonly TimesheetDb _context;

        public TimeSheetEntryService(TimesheetDb context)
        {
            _context = context;

        }

        /// <summary>
        /// Add single new time sheet entry
        /// </summary>
        /// <param name="timeSheetEntry"></param>
        /// <returns></returns>
        public int AddTimeSheetEntry(TimeSheetEntry timeSheetEntry)
        {
            _context.Add(timeSheetEntry);
            return _context.SaveChanges();
        }

        /// <summary>
        /// Add bulk timesheet enteries using AddRange
        /// </summary>
        /// <param name="timeSheetEntry"></param>
        public void AddTimeSheetEntry(List<TimeSheetEntry> timeSheetEntry)
        {
            _context.AddRange(timeSheetEntry);
            _context.SaveChanges();
        }

        /// <summary>
        /// Get all timesheet enteries
        /// </summary>
        /// <returns></returns>
        public List<TimeSheetEntry> GetAll()
        {
            return _context.TimeSheetEntries.ToList();
        }

        /// <summary>
        /// Get all timesheet enteries by employee id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public List<TimeSheetEntry> GetTimeSheetEnteries(int employeeId, int weekNo)
        {
            return _context.TimeSheetEntries
                .Where(x => x.EmployeeId == employeeId && x.WeekNo == weekNo)
                .ToList();
        }

        /// <summary>
        /// Update timesheet enteries for each task
        /// </summary>
        /// <param name="timeSheetEntry"></param>
        /// <returns></returns>
        public int UpdateUpdateTimeSheetEntry(TimeSheetEntry timeSheetEntry)
        {
            int result = 0;
            try
            {

                TimeSheetEntry entity = _context.TimeSheetEntries.FirstOrDefault(item => item.Id == timeSheetEntry.Id);

                if (entity != null)
                {
                    _context.TimeSheetEntries.Attach(entity);
                    entity.HoursWorked = timeSheetEntry.HoursWorked;
                    entity.TaskId = timeSheetEntry.TaskId;
                    entity.EmployeeId = timeSheetEntry.EmployeeId;
                    entity.DayOfWeek = timeSheetEntry.DayOfWeek;
                    entity.WeekNo = timeSheetEntry.WeekNo;
                    result = _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.Write("Something went wrong while updating data.");
                throw;
            }

            return result;
        }
    }
}
