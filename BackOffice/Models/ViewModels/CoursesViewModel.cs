using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BackOffice.Models.Domain;

namespace BackOffice.Models.ViewModels
{
    public class CoursesViewModel
    {
        public List<Course> Courses { get; set; }
        public string ClassTitle { get; set; }
        public int ClassId { get; set; }
    }
}