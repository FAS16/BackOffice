using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BackOffice.Models.Domain;

namespace BackOffice.Models.ViewModels
{
    public class StudentsViewModel
    {
        public List<Student> Students { get; set; }
        public string ClassTitle { get; set; }
    }
}