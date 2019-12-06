using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RegistrationEngine.Models.Domain
{
    public class Course
    {

        public int Id { get; set; }

        public string Title { get; set; }


        public string Description { get; set; }

        public Teacher Teacher { get; set; }

        // List if more than one day of the week
        public Lesson Lesson { get; set; }
    }
}