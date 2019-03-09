using System.Collections.Generic;
using timesheet.model;

namespace timesheet.business.Interface
{
    public interface ITimeSheetEntryService
    {
        List<TimeSheetEntry> GetAll();
        int AddTimeSheetEntry(TimeSheetEntry timeSheetEntry);
        void AddTimeSheetEntry(List<TimeSheetEntry> timeSheetEntry);
        int UpdateUpdateTimeSheetEntry(TimeSheetEntry timeSheetEntry);
        List<TimeSheetEntry> GetTimeSheetEnteries(int employeeId, int weekNo);
    }
}
