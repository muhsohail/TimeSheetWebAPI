using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace timesheet.api.ViewModel
{
    public class WeeklyEffortViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public double? TotalWeekEffort { get; set; }
        public double? AverageWeekEffort { get; set; }
    }
}
