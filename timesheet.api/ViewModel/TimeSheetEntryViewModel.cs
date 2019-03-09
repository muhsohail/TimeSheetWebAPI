using System.Collections.Generic;

namespace timesheet.api.ViewModel
{
    public class TimeSheetEntryViewModel
    {
        public TimeSheetEntryViewModel()
        {
            BreakdownByTask = new List<BreakdownByTask>();
        }
        public int TaskId { get; set; }
        public int WeekNo { get; set; }
        public int EmployeeId { get; set; }

        public List<BreakdownByTask> BreakdownByTask { get; set; }
    }

    public class BreakdownByTask
    {
        //public string DayName { get; set; }
        public double? HoursWorked { get; set; }
        public int DayNo { get; set; }
        public int Id { get; set; }
    }
}
