using System.Collections.Generic;

namespace BackOffice.Models.Domain
{
    public class Class
    {
        public int Id { get; set; }
        public string Title { get; set; }


        public List<Course> Courses { get; set; }
        public List<Student> Students { get; set; }
    }
}