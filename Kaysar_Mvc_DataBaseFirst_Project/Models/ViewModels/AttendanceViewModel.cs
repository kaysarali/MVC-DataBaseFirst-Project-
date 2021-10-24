using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kaysar_Mvc_DataBaseFirst_Project.Models.ViewModels
{
    public class AttendanceViewModel
    {
        public int AttendanceId { get; set; }
        public DateTime AttDate { get; set; }
        public TimeSpan LoginTime { get; set; }
        public TimeSpan LogoutTime { get; set; }
        public TimeSpan Late { get; set; }
        public TimeSpan OverTime { get; set; }
        public TimeSpan PartTime { get; set; }
        public int EmployeeId { get; set; }
        public string EmpName { get; set; }
    }
}