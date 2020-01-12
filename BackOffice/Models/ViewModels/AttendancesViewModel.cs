using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BackOffice.Models.Domain;

namespace BackOffice.Models.ViewModels
{
    public class AttendancesViewModel
    {
        public List<Attendance> Attendances { get; set; }
        public string ClassTitle { get; set; }
        public string CourseTitle { get; set; }
    }
}