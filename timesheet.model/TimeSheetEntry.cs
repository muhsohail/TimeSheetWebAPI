using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace timesheet.model
{
    public class TimeSheetEntry
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public double? HoursWorked { get; set; } 

        public int WeekNo { get; set; }

        [DisplayName("Task")]
        [ForeignKey("Task")]
        public int TaskId { get; set; }
        public Task Task { get; set; }

        [DisplayName("Employee")]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

    }
}
